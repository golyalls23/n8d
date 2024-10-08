﻿using N8D.Entities;

namespace N8D.Repos.Interfaces;

public interface ITodoRepository
{
    Task<TodoItem?> Create(TodoItem todoItem);
    Task<bool> Delete(int id);
    Task<List<TodoItem>> GetAll();
    Task<TodoItem?> GetById(int id);
    Task<TodoItem?> Update(int id, TodoItem todoItem);
    Task<int> MaxId();
}