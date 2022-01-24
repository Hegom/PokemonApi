using Moq;
using PokemonApi.Core.Model;
using PokemonApi.Infrastructure;
using Xunit;

namespace PokemonApi.Test
{
    public class PokemonTest
    {
        private readonly IPokemonRepository _companyProfileQueries;
        private readonly PokemonService _subject;

        public PokemonTest()
        {
            _companyProfileQueries = Mock.Of<IPokemonRepository>();
            _subject = new PokemonService(_companyProfileQueries);
        }

        [Fact]
        public async Task ShouldCalculateAnnualSalaryForHourlyContract()
        {
            var fakeUserId = 99;
            var page = 0;
            var pageSize = 10;

            var fakePokemons = new List<Pokemon>() {
               new Pokemon { Id = 1, Name = "Zapdos", Evolution = "None", Weight = 100, Width = 100, CreatedyByUserId = 90, IsPublic = true },
               new Pokemon { Id = 2, Name = "Gyarados", Evolution = "None", Weight = 100, Width = 100, CreatedyByUserId = 95, IsPublic = true },
               new Pokemon { Id = 3, Name = "Bulbasaur", Evolution = "Ivysaur", Weight = 100, Width = 100, CreatedyByUserId = 99, IsPublic = true },
               new Pokemon { Id = 4, Name = "Eevee", Evolution = "Vaporeon, Jolteon, Flareon", Weight = 100, Width = 100, CreatedyByUserId = 99, IsPublic = true }
                };

            Mock.Get(_companyProfileQueries)
                .Setup(x => x.GetAll(page, pageSize))
                .ReturnsAsync(fakePokemons);

            Mock.Get(_companyProfileQueries)
               .Setup(x => x.GetAllByUser(fakeUserId, page, pageSize))
               .ReturnsAsync(fakePokemons);

            var allPokemons = (await _subject.GetAll(page, pageSize))?.ToList();
            var userPokemons = (await _subject.GetAllByUser(fakeUserId, page, pageSize))?.ToList();

            Assert.NotNull(allPokemons);
            Assert.True(allPokemons.Count == 4);
            Assert.NotNull(userPokemons);
            Assert.True(userPokemons.Count == 4);
        }       
    }
}
