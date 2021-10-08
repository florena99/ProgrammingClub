using ProgrammingClub.Models;
using ProgrammingClub.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingClub.Repositories
{
    public class MembershipTypeRepository
    {
        private ClubMembershipModelsDataContext dbContext;

        public MembershipTypeRepository()
        {
            this.dbContext = new ClubMembershipModelsDataContext();
        }

        public MembershipTypeRepository(ClubMembershipModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void InsertMembershipType(MembershipTypeModel membershipTypeModel)
        {
            membershipTypeModel.IDMembershipType = Guid.NewGuid();
            dbContext.MembershipTypes.InsertOnSubmit(MapModelToDbObject(membershipTypeModel));
            dbContext.SubmitChanges();
        }

        public List<MembershipTypeModel> GetAllMembershipTypes()
        {
            List<MembershipTypeModel> membershipTypesList = new List<MembershipTypeModel>();
            foreach(var dbMembershipType in dbContext.MembershipTypes)
            {
                membershipTypesList.Add(MapDbObjectToModel(dbMembershipType));
            }
            return membershipTypesList;
        }

        public void UpdateMembershipType(MembershipTypeModel membershipTypeModel)
        {
            MembershipType existingMembershipType = dbContext.MembershipTypes.FirstOrDefault(x => x.IDMembershipType == membershipTypeModel.IDMembershipType);
            if (existingMembershipType != null)
            {
                existingMembershipType.IDMembershipType = membershipTypeModel.IDMembershipType;
                existingMembershipType.Name = membershipTypeModel.Name;
                existingMembershipType.Description = membershipTypeModel.Description;
                existingMembershipType.SubscriptionLengthInMonths = membershipTypeModel.SubscriptionLengthInMonths;
                dbContext.SubmitChanges();
            }
        }

        public MembershipTypeModel GetMembershipTypeById(Guid id)
        {
            return MapDbObjectToModel(dbContext.MembershipTypes.FirstOrDefault(mt => mt.IDMembershipType == mt.IDMembershipType));
        }

        public void DeleteMembershipType(Guid id)
        {
            MembershipType membershipTypeToBeDeleted = dbContext.MembershipTypes.FirstOrDefault(del => del.IDMembershipType == id);
            if( membershipTypeToBeDeleted != null)
            {
                dbContext.MembershipTypes.DeleteOnSubmit(membershipTypeToBeDeleted);
                dbContext.SubmitChanges();
            }
        }
        private MembershipTypeModel MapDbObjectToModel(MembershipType dbMembershipType)
        {
            MembershipTypeModel membershipTypeModel = new MembershipTypeModel();
            if(dbMembershipType != null)
            {
                membershipTypeModel.IDMembershipType = dbMembershipType.IDMembershipType;
                membershipTypeModel.Name = dbMembershipType.Name;
                membershipTypeModel.Description = dbMembershipType.Description;
                membershipTypeModel.SubscriptionLengthInMonths = dbMembershipType.SubscriptionLengthInMonths;
                return membershipTypeModel;
            }
            return null;
        }

        private MembershipType MapModelToDbObject(MembershipTypeModel membershipTypeModel)
        {
            MembershipType dbmembershipType = new MembershipType();
            if(membershipTypeModel != null)
            {
                dbmembershipType.IDMembershipType = membershipTypeModel.IDMembershipType;
                dbmembershipType.Name = membershipTypeModel.Name;
                dbmembershipType.Description = membershipTypeModel.Description;
                dbmembershipType.SubscriptionLengthInMonths = membershipTypeModel.SubscriptionLengthInMonths;
                return dbmembershipType;

            }
            return null;
        }
    }
}