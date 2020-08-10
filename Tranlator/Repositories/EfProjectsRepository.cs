using System;
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

        public async Task<Project> CreateProject(User owner, string name, Lang mainLang)
        {
            var project = new Project { Owner = owner, Name = name, IsPublic = false, MainLang = mainLang};
            await _ctx.AddAsync(project);
            return project;
        }

        public async Task<List<Project>> GetUserProjects(int userId)
        {
            try
            {
                await _ctx.Users.FirstAsync(user => user.Id.Equals(userId));
            }
            catch (InvalidOperationException)
            {
                throw new RecordNotFoundException("user");
            }
            return 
                await _ctx
                    .Projects
                    .Where(proj => proj.Owner.Id.Equals(userId))
                    .Include(proj => proj.MainLang)
                    .ToListAsync();
        }

        public async Task<List<Lang>> GetProjectLangs(int projectId)
        {
            return await _ctx.Langs.Where(lang => lang.Project.Id.Equals(projectId)).ToListAsync();
        }

        public async Task<List<File>> GetLangFiles(int langId)
        {
            return await _ctx.Files.Where(file => file.Lang.Id.Equals(langId)).ToListAsync();
        }

        public async Task<List<Paragraph>> GetFileParagraphs(int fileId)
        {
            return await _ctx.Paragraphs.Where(paragraph => paragraph.File.Id.Equals(fileId)).ToListAsync();
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

        public async Task<User> GetParagraphAuthor(int paragraphId)
        {
            try
            {
                await _ctx.Paragraphs.FirstAsync(p => p.Id.Equals(paragraphId));
            }
            catch (InvalidOperationException)
            {
                throw new RecordNotFoundException("paragraph");
            }

            return await _ctx.Users.FirstAsync(
                user => user.Projects.Any(
                    proj => proj.Langs.Any(
                        lang => lang.Files.Any(file => file.Paragraphs.Any(
                            paragraph => paragraph.Id.Equals(paragraphId)
                            ))
                    )
                )
            );
        }

        public async Task AddFile(int langId, File file)
        {
            try
            {
                var lang = await _ctx.Langs.FirstAsync(lang => lang.Id.Equals(langId));
                lang.Files.Add(file);
            }
            catch (InvalidOperationException)
            {
                throw new RecordNotFoundException("lang");
            }
        }

        public async Task AddLang(int projectId, Lang lang)
        {
            try
            {
                var prj = await _ctx.Projects.FirstAsync(prj => prj.Id.Equals(projectId));
                prj.Langs.Add(lang);
            }
            catch (InvalidOperationException)
            {
                throw new RecordNotFoundException("project");
            }
        }

        public async Task AddParagraph(int fileId, Paragraph paragraph)
        {
            try
            {
                var file = await _ctx.Files.FirstAsync(file => file.Id.Equals(fileId));
                file.Paragraphs.Add(paragraph);
            }
            catch (InvalidOperationException)
            {
                throw new RecordNotFoundException("file");
            }
        }
    }
}