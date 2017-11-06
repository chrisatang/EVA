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
            if (activity.Text.ToLower().Contains("how many professors"))
            {      
                Professors profs = new Professors();
                await context.PostAsync($"There are { profs.get_professor_count() } professors.");
            }

            else if (activity.Text.ToLower().Contains("how many classes"))
            {
                Professors profs = new Professors();
                string prof_name = "";
                char[] delim = { ' ' };
                string[] in_arr = in_text.Split(delim);
                for (int i=0; i< in_arr.Length; i++)
                {
                   if (in_arr[i].ToLower() == "dr." || in_arr[i].ToLower() == "professor")
                       prof_name = in_arr[i + 1];
                }
                await context.PostAsync($"Professor { prof_name } teaches { profs.get_num_classes_taught(prof_name) } classes.");

                List<string> classes = profs.get_classes(name: prof_name);
                await context.PostAsync($"They are: "  );
                foreach (var class_ in classes)
                {
                    string cls = class_;
                    await context.PostAsync($" { cls }");
                }

                await context.PostAsync($"You said: **{ in_text }** ");
  
            }

            else
            {
                await context.PostAsync($"Sorry I don't know how to answer that yet.");
            }

            context.Wait(ActivityReceivedAsync);
        }
    }
}