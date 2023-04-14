using Microsoft.EntityFrameworkCore;
using TaskManager.Server.Entity;
using TaskManager.Shared;
using TaskManager.Shared.Models;

namespace TaskManager.Server.Services;

public class TaskService
{
    private ApplicationContext Context { get; set; }
    private DbSet<UserTask>  DbSetUserTask { get; set; }

    public TaskService(ApplicationContext context)
    {
        Context = context;
        DbSetUserTask = context.Set<UserTask>();
    }
    
    
    public async Task Create(UserTask obj)
    {
        try
        {
            await DbSetUserTask.AddAsync(obj);
            await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            var sw = e.Message;
        }
    }

    public async Task<List<UserTask>> GetItems()
    {
        try
        {
            return await DbSetUserTask.ToListAsync();
            
        }
        catch (Exception e)
        {
            var sw = e.Message;
            return default;
        }
    }
    public async Task<List<UserTask>> GetItemsById(string id)
    {
        try
        {
            return await DbSetUserTask.Where(o=>o.ToUserId==id).ToListAsync();
            
        }
        catch (Exception e)
        {
            var sw = e.Message;
            return default;
        }
    }

    public async Task Update(UpdateTask obj)
    {
        try
        {
            var founded =  DbSetUserTask.FirstOrDefault(x=>x.Id.ToString()==obj.Id);
            if (founded is not null)
            {
                founded.IsComplete = obj.IsComplete;
                Context.Update(founded);
                await Context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("объект не найден");
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public void Delete(string id)
    {
        var obj = DbSetUserTask.Find(id);
        if (obj is not null)
        {
            DbSetUserTask.Remove(obj);
        }
        else
        {
            throw new Exception("объект не был найден");
        }
    }

  
}