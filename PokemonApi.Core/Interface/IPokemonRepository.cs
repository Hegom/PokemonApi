﻿using PokemonApi.Core.Model;

namespace PokemonApi.Infrastructure
{
    public interface IPokemonRepository
    {
        Task<bool> Create(User user);
        Task<User> Find(string email);
        Task<bool> Update(int id, User user);
        Task<User> Get(int id);
        Task<(bool, string)> Register(User user);
        Task<AuthResponse?> Authenticate(AuthRequest authRequest);
    }
}
