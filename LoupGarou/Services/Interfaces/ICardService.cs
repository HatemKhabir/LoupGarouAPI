using LoupGarou.Model;

namespace LoupGarou.Services.Interfaces
{
    public interface ICardService
    {
        Task<Card?> CreateCard(CreateCardRequest request);
        Task<IEnumerable<Card>?> GetAllCards();
        Task<Card?> GetCard(Guid cardId);
        Task DeleteCard(Guid cardId);
    }
}
