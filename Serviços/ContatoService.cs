using System;
using System.Collections.Generic;

namespace MoneyTrack.Models;

public interface IContatoService
{
    List<Contato> ObterTodos();
}

// ContatoService.cs
public class ContatoService : IContatoService
{
    private readonly MoneyTrackRepository _repository;

    public ContatoService(MoneyTrackRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public List<Contato> ObterTodos()
    {
        return _repository.Listar();
    }
}