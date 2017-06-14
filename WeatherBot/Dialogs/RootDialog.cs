using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            CurrentConditions conditions = await OpenWeather.GetWeatherAsync(activity.Text);
 
            // return our reply to the user
            await context.PostAsync(string.Format("Current conditions in {1}: {0}. The temperature is {2}\u00B0 F.", conditions.Weather[0].Main, conditions.CityName, conditions.Main.Temperature));

            context.Wait(MessageReceivedAsync);
        }
    }
}