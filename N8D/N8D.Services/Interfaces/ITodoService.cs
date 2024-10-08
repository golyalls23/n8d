﻿using N8D.Models;

namespace N8D.Services.Interfaces;

public interface ITodoService
{
    Task<List<TodoItem>> GetAll();
    Task<TodoItem?> GetById(int id);
    Task<TodoItem?> Create(TodoItemCreate todoItemCreate);
    Task<TodoItem?> Update(int id, TodoItemUpdate todoItemUpdate);
    Task<bool> Delete(int id);
}
