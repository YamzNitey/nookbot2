﻿using System;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace nookbot2
{
    class CommandHandler
    {
        public static DiscordSocketClient Client;
        private CommandService Service;

        public async Task InitAsync(DiscordSocketClient client)
        {
            Client = client;
            Service = new CommandService();
            await Service.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);

            Client.MessageReceived += HandleCmdAsync;
            Client.ReactionAdded += Client_ReactionAdded;
            Client.ReactionRemoved += Client_ReactionRemoved;
        }

        private async Task HandleCmdAsync(SocketMessage message)
        {
            if (!(message is SocketUserMessage userMessage)) return;
            SocketCommandContext context = new SocketCommandContext(Client, userMessage);

            int argPos = 0;
            if(userMessage.HasStringPrefix(Config.Bot.CommandPrefix, ref argPos))
                await Service.ExecuteAsync(context, argPos, services: null);
        }

        private async Task Client_ReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                ReactionModLog reactionModLog = new ReactionModLog()
                {
                    Title = "__Reaction added__",
                    UserReaction = $"{reaction.User} ({reaction.UserId})",
                    ChannelID = channel.Id,
                    Emote = reaction.Emote,
                    DTAndMID = $"Message ID: {message.Id} | {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm tt")} (UTC)"
                };

                await Log.SendReactionLog(reactionModLog);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task Client_ReactionRemoved(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                ReactionModLog reactionModLog = new ReactionModLog()
                {
                    Title = "__Reaction removed__",
                    UserReaction = $"{reaction.User} ({reaction.UserId})",
                    ChannelID = channel.Id,
                    Emote = reaction.Emote,
                    DTAndMID = $"Message ID: {message.Id} | {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm tt")} (UTC)"
                };

                await Log.SendReactionLog(reactionModLog);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}