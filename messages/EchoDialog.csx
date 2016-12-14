using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

// For more information about this template visit http://aka.ms/azurebots-csharp-basic
[Serializable]
public class EchoDialog : IDialog<object>
{
    protected int count = 1;
    String[] clients = new String[10] {"user", "user", "user", "user", "user", "user", "user", "user", "user", "user"};
    Boolean didntAskAboutPenisSize = true;

    public Task StartAsync(IDialogContext context)
    {
        try
        {
            context.Wait(MessageReceivedAsync);
        }
        catch (OperationCanceledException error)
        {
            return Task.FromCanceled(error.CancellationToken);
        }
        catch (Exception error)
        {
            return Task.FromException(error);
        }

        return Task.CompletedTask;
    }

    public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    {
        var message = await argument;
        
        if (message.Text == "reset")
        {
            PromptDialog.Confirm(
                context,
                AfterResetAsync,
                "Are you sure you want to reset the count?",
                "Didn't get that!",
                promptStyle: PromptStyle.Auto);
        }
        else
        {
            
            String nadawca = message.From.Name;
            String text = message.Text;
            Boolean alreadyWritten = false;
            for (int i = 0; i < 10; i++) {
                if(clients[i].Equals(nadawca))
                {
                    alreadyWritten = true;
                    break;
                }
            }
            
            if (!alreadyWritten)
            {
                if (nadawca.Equals("Kamil Augustyn"))
                {
                    await context.PostAsync("Augustyn Kamil sie poplamil");
                    this.count++;
                }
                else if (nadawca.Equals("Krzystof Krawczyk"))
                {
                    await context.PostAsync("Krzysztof Krawczyk to sprzedawczyk!");
                    this.count++;
                }
                else if (nadawca.Equals("Filip Biedrzycki"))
                {
                    await context.PostAsync("21:37: Filip Biedrzycki ma duze cycki!");
                    this.count++;
                }
                this.clients[count] = nadawca;
                didntAskAboutPenisSize = true;
                await context.PostAsync("Co tam u kolegi?");
                await context.PostAsync("Zmalal urus?");
            }
            else if (didntAskAboutPenisSize)
            {
               
                
                if (text.Equals("zmalal")) {
                    await context.PostAsync("Przykro mi :(");
                    didntAskAboutPenisSize = false;
                } else if(text.Equals("urus"))
                {
                    await context.PostAsync("GRATULACJE!");
                    didntAskAboutPenisSize = false;
                } else if(text.Equals("moglby zmalec"))
                {
                    await context.PostAsync("Pytam o pindola, nie maæka...");
                    didntAskAboutPenisSize = false;
                }
                
                
            } else
            {
                await context.PostAsync("Milo sie gadalo, na razie!");
            }
            context.Wait(MessageReceivedAsync);
        }
    }

    public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
    {
        var confirm = await argument;
        if (confirm)
        {
            this.count = 1;
            await context.PostAsync("Reset count.");
        }
        else
        {
            await context.PostAsync("Did not reset count.");
        }
        context.Wait(MessageReceivedAsync);
    }
}