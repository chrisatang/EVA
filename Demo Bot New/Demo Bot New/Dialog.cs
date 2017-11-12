using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


/* Returns valid output for user input via Microsoft Bot Framework */
namespace Bot_Application
{
    [Serializable]
    public class Dialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(ActivityReceivedAsync);
        }

        private async Task ActivityReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            string in_text = activity.Text.ToLower();
            if (activity.Text.ToLower() == "hi" || activity.Text.ToLower() == "hello" || activity.Text.ToLower() == "yo")
                await context.PostAsync($"What's good dog ask me a question");
            else if (activity.Text.ToLower().Contains("how many professors"))
            {
                Professors profs = new Professors();
                await context.PostAsync($"I know about { profs.get_professor_count() } professors.");
            }

            else if (activity.Text.ToLower().Contains("how many classes") || activity.Text.ToLower().Contains("which classes"))
            {
                Professors profs = new Professors();
                string prof_name = "";
                char[] delim = { ' ' };
                string[] in_arr = in_text.Split(delim);
                bool found = false;
                for (int i = 0; i < in_arr.Length; i++)
                {
                    if (in_arr[i].ToLower() == "dr." || in_arr[i].ToLower() == "professor")
                    {
                        prof_name = in_arr[i + 1];
                        found = true;
                    }
                }
                if (found == true)
                {
                    await context.PostAsync($"Professor { prof_name } teaches { profs.get_num_classes_taught(prof_name) } classes.");
                    List<string> classes = profs.get_classes(name: prof_name);
                    string buff = "";
                    //await context.PostAsync($"They are: ");
                    int count = 0;
                    foreach (var class_ in classes)
                    {
                        string cls = class_;
                        if (count < classes.Count - 1)
                            buff += (cls.ToUpper() + ", ");
                        else
                            buff += "and " + cls.ToUpper() + ".";
                        ++count;
                        //await context.PostAsync($" { cls }");
                    }
                    //await context.PostAsync($"You said: **{ in_text }** ");
                    await context.PostAsync($"They are {buff}");
                }

                else
                    await context.PostAsync($"Sorry I don't understand that");

            }
            else if (activity.Text.ToLower().Contains("which professors"))
            {
                string buff = "";
                int count = 0;
                Professors profs = new Professors();
                foreach (var key in profs.get_keys())
                {
                    if (count < profs.get_keys().Count - 1)
                        buff += (key + ", ");
                    else
                        buff += ("and " + key + ".");
                    ++count;
                }
                await context.PostAsync($"The professors I know are { buff }");
            }

            else
            {
                await context.PostAsync($"Sorry I don't know how to answer that yet.");
            }

            context.Wait(ActivityReceivedAsync);
        }
    }
}
