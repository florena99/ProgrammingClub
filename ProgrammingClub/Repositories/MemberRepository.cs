using ProgrammingClub.Models;
using ProgrammingClub.Models.DBObjects;
using ProgrammingClub.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingClub.Repositories
{
    public class MemberRepository
    {
        private ClubMembershipModelsDataContext dbContext;

        public MemberRepository()
        {
            this.dbContext = new ClubMembershipModelsDataContext();
        }

        public MemberRepository(ClubMembershipModelsDataContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public MemberCodeSnippetsViewModel GetMemberCodeSnippets(Guid memberID)
        {
            MemberCodeSnippetsViewModel memberCodeSnippetsViewModel = new MemberCodeSnippetsViewModel();
            Member member = dbContext.Members.FirstOrDefault(m => m.IDMember == memberID);

            if(member != null)
            {
                memberCodeSnippetsViewModel.Name = member.Name;
                memberCodeSnippetsViewModel.Title = member.Title;
                memberCodeSnippetsViewModel.Position = member.Position;

                IQueryable<CodeSnippet> memberCodeSnippets = dbContext.CodeSnippets.Where(c => c.IDMember == member.IDMember);
                foreach( CodeSnippet code in memberCodeSnippets)
                {
                    CodeSnippetModel codeSnippetModel = new CodeSnippetModel();
                    codeSnippetModel.Title = code.Title;
                    codeSnippetModel.ContentCode = code.ContentCode;
                    codeSnippetModel.Revision = code.Revision;
                    memberCodeSnippetsViewModel.CodeSnippets.Add(codeSnippetModel);
                }

            }
            return memberCodeSnippetsViewModel;
        }

        public void InsertMember(MemberModel memberModel)
        {
            memberModel.IDMember = Guid.NewGuid();
            dbContext.Members.InsertOnSubmit(MapModelToDbObject(memberModel));
            dbContext.SubmitChanges();
        }

        public List<MemberModel> GetAllMembers()
        {
            List<MemberModel> membersList = new List<MemberModel>();
            foreach(var dbMember in dbContext.Members)
            {
                membersList.Add(MapDbObjectToModel(dbMember));
            }
            return membersList;
        }

        public void UpdateMember(MemberModel memberModel)
        {
            Member existingMember = dbContext.Members.FirstOrDefault(m => m.IDMember == memberModel.IDMember);
            if(existingMember != null)
            {
                existingMember.IDMember = memberModel.IDMember;
                existingMember.Name = memberModel.Name;
                existingMember.Title = memberModel.Title;
                existingMember.Position = memberModel.Position;
                existingMember.Resume = memberModel.Resume;
                existingMember.Description = memberModel.Description;
                dbContext.SubmitChanges();
            }
        }

        public MemberModel GetMemberById(Guid id)
        {
            return MapDbObjectToModel(dbContext.Members.FirstOrDefault(m => m.IDMember == id));
        }

        public void DeleteMember(Guid id)
        {
            Member memberToBeDeleted = dbContext.Members.FirstOrDefault(m => m.IDMember == id);
            if(memberToBeDeleted != null)
            {
                dbContext.Members.DeleteOnSubmit(memberToBeDeleted);
                dbContext.SubmitChanges();
            }
        }

        private MemberModel MapDbObjectToModel(Member dbMember)
        {
            MemberModel memberModel = new MemberModel();
            if( dbMember != null)
            {
                memberModel.IDMember = dbMember.IDMember;
                memberModel.Name = dbMember.Name;
                memberModel.Title = dbMember.Title;
                memberModel.Position = dbMember.Position;
                memberModel.Description = dbMember.Description;
                memberModel.Resume = dbMember.Resume;
                return memberModel;
            }
            return null;
        }

        private Member MapModelToDbObject(MemberModel memberModel)
        {
            Member dbMember = new Member();
            if(memberModel != null)
            {
                dbMember.IDMember = memberModel.IDMember;
                dbMember.Name = memberModel.Name;
                dbMember.Title = memberModel.Title;
                dbMember.Position = memberModel.Position;
                dbMember.Description = memberModel.Description;
                dbMember.Resume = memberModel.Resume;
                return dbMember;
            }
            return null;
        }
    }
}