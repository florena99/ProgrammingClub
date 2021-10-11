using ProgrammingClub.Models;
using ProgrammingClub.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingClub.Repositories
{
    public class CodeSnippetRepository
    {

        private ClubMembershipModelsDataContext dbContext;

        public CodeSnippetRepository()
        {
            this.dbContext = new ClubMembershipModelsDataContext();
        }

        public CodeSnippetRepository(ClubMembershipModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private CodeSnippetModel MapDbObjectToModel(CodeSnippet dbCodeSnippet)
        {
            CodeSnippetModel codeSnippetModel = new CodeSnippetModel();
            if (dbContext != null)
            {
                codeSnippetModel.IDCodeSnippet = dbCodeSnippet.IDCodeSnippet;
                codeSnippetModel.ContentCode = dbCodeSnippet.ContentCode;
                codeSnippetModel.IDMember = dbCodeSnippet.IDMember;
                codeSnippetModel.Title = dbCodeSnippet.Title;
                codeSnippetModel.Revision = dbCodeSnippet.Revision;
                return codeSnippetModel;
            }
            return null;
        }

        private CodeSnippet MapModelToDbObject(CodeSnippetModel codeSnippetModel)
        {
            CodeSnippet codeSnippet = new CodeSnippet();
            if (codeSnippetModel != null)
            {
                codeSnippet.IDCodeSnippet = codeSnippetModel.IDCodeSnippet;
                codeSnippet.ContentCode = codeSnippetModel.ContentCode;
                codeSnippet.IDMember = codeSnippetModel.IDMember;
                codeSnippet.Title = codeSnippetModel.Title;
                codeSnippet.Revision = codeSnippetModel.Revision;
                return codeSnippet;
            }
            return null;
        }

        public List<CodeSnippetModel> GetAll()
        {
            List<CodeSnippetModel> codeSnippetModels = new List<CodeSnippetModel>();
            foreach(var codesnippet in dbContext.CodeSnippets)
            {
                codeSnippetModels.Add(MapDbObjectToModel(codesnippet));
            }

            return codeSnippetModels;
        }

        public CodeSnippetModel GetById(Guid ID)
        {
            return MapDbObjectToModel(dbContext.CodeSnippets.FirstOrDefault(cs => cs.IDCodeSnippet == ID));


        }

        public void InsertCodeSnippet( CodeSnippetModel codeSnippetModel)
        {
            codeSnippetModel.IDCodeSnippet = Guid.NewGuid();
            dbContext.CodeSnippets.InsertOnSubmit(MapModelToDbObject(codeSnippetModel));
            dbContext.SubmitChanges();
        }

        public void UpdateCodeSnippet(CodeSnippetModel codeSnippetModel)
        {
            CodeSnippet codeSnippet = dbContext.CodeSnippets.FirstOrDefault(c => c.IDCodeSnippet == codeSnippetModel.IDCodeSnippet);
            if (codeSnippet!= null)
            {
                codeSnippet.IDCodeSnippet = codeSnippetModel.IDCodeSnippet;
                codeSnippet.ContentCode = codeSnippetModel.ContentCode;
                codeSnippet.IDMember = codeSnippetModel.IDMember;
                codeSnippet.Title = codeSnippetModel.Title;
                codeSnippet.Revision = codeSnippetModel.Revision;
                dbContext.SubmitChanges();

            }
        }

        public void Delete(Guid id)
        {
            CodeSnippet codeSnippet = dbContext.CodeSnippets.FirstOrDefault(c => c.IDCodeSnippet == id);
            if (codeSnippet != null)
            {
                dbContext.CodeSnippets.DeleteOnSubmit(codeSnippet);
                dbContext.SubmitChanges();
            }
        }
    }
}