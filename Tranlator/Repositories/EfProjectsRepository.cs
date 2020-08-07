﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tranlator.Exceptions;
using Tranlator.Models;

namespace Tranlator.Repositories
{
    public class EfProjectsRepository : IProjectsRepository
    {
        private TranslatorContext _ctx;

        public EfProjectsRepository(TranslatorContext ctx)
        {
            _ctx = ctx;
        }
        
        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task<Project> CreateProject(User owner, string name)
        {
            var project = new Project { Owner = owner, Name = name, IsPublic = false };
            await _ctx.AddAsync(project);
            return project;
        }

        public async Task<List<Project>> GetUserProjects(string username)
        {
            try
            {
                await _ctx.Users.FirstAsync(user => user.Name.Equals(username));
            }
            catch (InvalidOperationException)
            {
                throw new RecordNotFoundException("user");
            }
            return await _ctx.Projects.Where(proj => proj.Owner.Name.Equals(username)).ToListAsync();
        }

        public async Task UpdateParagraph(int paragraphId, string newContent)
        {
            try
            {
                var paragraph = await _ctx.Paragraphs.FirstAsync(par => par.Id.Equals(paragraphId));
                paragraph.Content = newContent;
            }
            catch (InvalidOperationException)
            {
                throw new RecordNotFoundException("user");
            }
        }
    }
}