using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System;

namespace nookbot2.Modules
{
    public class Moderator : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(SocketGuildUser user, [Remainder]string reason = "No reason provided.")
        {
            if (Context.User == user)
            {
                await ReplyAsync("You cannot perform moderator actions on yourself. 😡");
            }
            else
            {
                try
                {
                    await user.KickAsync(reason: $"Kicked by: {Context.User} | Reason: {reason}");
                    await ReplyAsync($"`{user}` has been kicked.");
                }
                catch (Exception e)
                {
                    await ReplyAsync($"An error occured while attempting to kick `{user}`.");
                    Console.WriteLine(e);
                }
            }
        }

        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(SocketGuildUser user, [Remainder]string reason = "No reason provided.")
        {
            // If the user is executing the command on themselves...
            if (Context.User == user)
            {
                await ReplyAsync("You cannot perform moderator actions on yourself. 😡");
            }
            else
            {
                try
                {
                    await user.BanAsync(reason: $"Banned by: {Context.User} | Reason: {reason}");
                    await ReplyAsync($"`{user}` has been banned. 🔨");
                }
                catch (Exception e)
                {
                    await ReplyAsync($"An error occured while attempting to ban `{user}`.");
                    Console.WriteLine(e);
                }
            }
        }

        [Command("unban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task UnbanUser(SocketGuildUser user)
        {
            try
            {
                await user.Guild.RemoveBanAsync(user);
                await ReplyAsync($"`{user}` has been unbanned.");
            }
            catch (Exception e)
            {
                await ReplyAsync($"An error occured while attempting to unban `{user}`.");
                Console.WriteLine(e);
            }
        }

        [Command("mute")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task MuteUser(SocketGuildUser user)
        {
            if (Context.User == user)
            {
                await ReplyAsync("You cannot perform moderator actions on yourself. 😡");
            }
            else
            {
                try
                {
                    IRole mutedRole = Context.Guild.Roles.FirstOrDefault(role => role.Name == "Muted");
                    await user.AddRoleAsync(mutedRole);
                    await ReplyAsync($"`{user}` has been muted.");
                }
                catch (Exception e)
                {
                    await ReplyAsync($"An error occured while attempting to mute `{user}`.");
                    Console.WriteLine(e);
                }
            }
        }

        [Command("unmute")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task UnmuteUser(SocketGuildUser user)
        {
            try
            {
                IRole mutedRole = Context.Guild.Roles.FirstOrDefault(role => role.Name == "Muted");
                if (user.Roles.Contains(mutedRole))
                {
                    await user.RemoveRoleAsync(mutedRole);
                    await ReplyAsync($"`{user}` has been unmuted.");
                }
                else
                {
                    await ReplyAsync($"{user} isn't muted.");
                }
            }
            catch (Exception e)
            {
                await ReplyAsync($"An error occured while attempting to unmute `{user}`.");
                Console.WriteLine(e);
            }
        }

        [Command("lock")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task LockChannel()
        {
            try
            {
                SocketGuildChannel channel = Context.Channel as SocketGuildChannel;
                await channel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, OverwritePermissions.InheritAll.Modify(sendMessages: PermValue.Deny));
                await ReplyAsync("Channel locked. 🔒");
            }
            catch (Exception e)
            {
                await ReplyAsync("An error occured while attempting to lock the channel.");
                Console.WriteLine(e);
            }
        }

        [Command("unlock")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task UnlockChannel()
        {
            try
            {
                SocketGuildChannel channel = Context.Channel as SocketGuildChannel;
                await channel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, OverwritePermissions.InheritAll.Modify(sendMessages: PermValue.Inherit));
                await ReplyAsync("Channel unlocked. 🔓");
            }

            catch (Exception e)
            {
                await ReplyAsync("An error occured while attempting to unlock the channel.");
                Console.WriteLine(e);
            }
        }
    }
}