﻿using Sprout.Web.Mappings;
using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Review;
using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Web.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task CreateCardAsync(string userId, string kanjiLiteral)
        {
            var card = new Card
            {
                UserId = userId,
                Kanji = kanjiLiteral,
                SrsData = new SrsData()
            };

            await _cardRepository.SaveCardAsync(card);
        }

        public async Task DeleteCardAsync(int cardId)
        {
            var card = await _cardRepository.GetCardByIdAsync(cardId);
            if (card == null)
            {
                return;
            }
            await _cardRepository.DeleteCardAsync(card);
        }

        public async Task<CardDto> GetCardByIdAsync(int cardId)
        {
            var card = await _cardRepository.GetCardByIdAsync(cardId);
            if (card == null)
            {
                throw new Exception("Card not found.");
            }
            return _mapper.MapCardToDto(card);
        }

        public async Task<CardDto> GetCardByKanjiAsync(string userId, string kanjiLiteral)
        {
            var card = await _cardRepository.GetCardByKanjiAsync(userId, kanjiLiteral);
            if (card == null)
            {
                throw new Exception("Card not found.");
            }
            return _mapper.MapCardToDto(card);
        }

        public async Task<List<CardDto>> GetDueCardsAsync(string userId, DateTime dueDateTime)
        {
            var newCards = (await _cardRepository.GetCardsWithoutReviewAsync(userId)).ToList();
            var dueCards = (await _cardRepository.GetCardsDueOnAsync(userId, dueDateTime)).ToList();

            dueCards.AddRange(newCards);

            List<CardDto> cardDtos = dueCards.Select(card => _mapper.MapCardToDto(card)).ToList();
            return cardDtos;
        }

        public async Task<CardReviewSummaryDto> GetReviewSummaryAsync(string userId, DateTime dueDateTime)
        {
            var newCount = await _cardRepository.GetCardsWithoutReviewCountAsync(userId);
            var dueCount = await _cardRepository.GetCardsDueOnCountAsync(userId, dueDateTime);
            var summary = new CardReviewSummaryDto
            {
                New = newCount,
                Due = dueCount
            };

            return summary;
        }
    }
}
