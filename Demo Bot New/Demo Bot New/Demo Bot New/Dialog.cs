using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                await context.PostAsync($"Hello! Ask me a question");
            else if (activity.Text.ToLower().Contains("how many professors"))
            {
                Professors profs = new Professors();
                await context.PostAsync($"I know about { profs.get_professor_count() } professors.");
            }
            else if (in_text.Contains("who teaches" ) || in_text.Contains("which professors teach"))
            {
                Professors profs = new Professors();
                string class_name = "";
                char[] delim = { ' ' };
                bool found = false;
                string[] in_arr = in_text.Split(delim);
                for (int i = 0; i < in_arr.Length; i++)
                {
                    if (in_arr[i].ToLower() == "teach" || in_arr[i].ToLower() == "teaches")
                    {
                        class_name = in_arr[i + 1];
                        found = true;
                    }
                }
                if (found == false) { await context.PostAsync($"I don't know that class yet."); }
                else
                {
                    List<string> teachers = profs.get_teachers(class_: class_name);
                    int count = 0;
                    string buff = "";
                    foreach (var teacher in teachers)
                    {
                        string t = teacher;
                        if (count < teachers.Count - 1 || teachers.Count == 1)
                            buff += (t.ToUpper() + ", ");
                        else
                            buff += "and " + t.ToUpper() + ".";
                        ++count;
                        //await context.PostAsync($" { t }");
                    }
                    //await context.PostAsync($"You said: **{ in_text }** ");
                    if (teachers.Count == 1)
                        await context.PostAsync($"{buff} teaches this class.");
                    else
                        await context.PostAsync($"{buff} teach this class.");

                }
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
                    await context.PostAsync($"I don't know that professor yet.");

            }
            else if (activity.Text.ToLower().Contains("which professors do you know") || in_text.Contains("how many pprofessors do you know"))
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