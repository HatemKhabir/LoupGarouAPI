using LoupGarou.Data;
using LoupGarou.Model;
using LoupGarou.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace LoupGarou.Services;
public class CardService : ICardService
{
    private readonly LoupGarouDbContext loupGarouDbContext;

    public CardService(LoupGarouDbContext loupGarouDbContext)
    {
        this.loupGarouDbContext = loupGarouDbContext;
    }

    public async Task<Card?> CreateCard(CreateCardRequest request)
    {
        if (request == null || request.CardName.IsNullOrEmpty()) return null;

        Card newCard = new Card()
        {
            CardId = new Guid(),
            CardName = request.CardName,
            Description = request.Description,
            ImageName = request.ImageName
        };
        loupGarouDbContext.Cards.Add(newCard);
        await loupGarouDbContext.SaveChangesAsync();

        return newCard;
    }

    public async Task<IEnumerable<Card>?> GetAllCards()
    {
        var allCards = await loupGarouDbContext.Cards.ToListAsync();
        return allCards;
    }

    public async Task<Card?> GetCard(Guid cardId)
    {
        var card = await loupGarouDbContext.Cards.FindAsync(cardId);
        return card;
    }

    public async Task DeleteCard(Guid cardId)
    {
        var card = await loupGarouDbContext.Cards.FindAsync(cardId);
        loupGarouDbContext.Cards.Remove(card);
        await loupGarouDbContext.SaveChangesAsync();
    }
}
