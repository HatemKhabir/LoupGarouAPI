using LoupGarou.Model;

namespace LoupGarou.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<Character> CreateCharacter(string request);
        Task<IEnumerable<Character>> GetAllCharacters();
        Task<Character> GetCharacter(string characterID);
        Task Deletecharacter(string characterID);
    }
}
