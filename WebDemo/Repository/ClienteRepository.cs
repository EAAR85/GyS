using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Common;
using WebDemo.Entity;
using WebDemo.Services;

namespace WebDemo.Repository
{
    public class ClienteRepository
    {
        private readonly TodoDBContext context;
        private readonly IMapper mapper;
        public ClienteRepository(TodoDBContext context,  IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ClienteResponse>> all()
        {
            var result = await context.ToDoItems.ToListAsync();
            var mapped = mapper.Map<List<ClienteResponse>>(result);
            return mapped;
        }

        public async Task<ClienteResponse> find(int id)
        {
            var result = await context.ToDoItems.FindAsync(id);
            if (result == null)
                throw new Exception("NotFound.");

            var mapped = mapper.Map<ClienteResponse>(result);

            return mapped;
        }

        public async Task update(int id, ClienteRequest request)
        {
            var mapped = mapper.Map<Cliente>(request);
            context.Entry(mapped).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task save(ClienteRequest request)
        {
            var mapped = mapper.Map<Cliente>(request);

            context.ToDoItems.Add(mapped);
            await context.SaveChangesAsync();
        }

        public async Task delete(int id)
        {
            var result = await context.ToDoItems.FindAsync(id);

            if (result == null)
                throw new Exception("NotFound.");

            context.ToDoItems.Remove(result);
            await context.SaveChangesAsync();
        }
    }
}
