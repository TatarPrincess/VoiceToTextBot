using System.Threading.Tasks;
using System.Threading;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceToTextBot.Configuration;
using VoiceToTextBot.Services;

namespace VoiceToTextBot.Controllers
{
    public class VoiceMessageController
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;
        private readonly IStorage _memoryStorage;

        public VoiceMessageController(AppSettings appSettings, ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler, IStorage memoryStorage)
        {
            _appSettings = appSettings;
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
            _memoryStorage = memoryStorage;
    }
        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;

            await _audioFileHandler.Download(fileId, ct);

            await _telegramClient.SendTextMessageAsync(message.Chat.Id, 
                                                       "Голосовое сообщение загружено", 
                                                       cancellationToken: ct);
            
            _audioFileHandler.Process(_memoryStorage.GetSession(message.From.Id).LanguageCode);

            await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                                                       "Голосовое сообщение сконвертировано",
                                                       cancellationToken: ct);
        }
    }
}