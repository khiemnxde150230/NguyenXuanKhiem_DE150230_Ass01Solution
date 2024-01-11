using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        private FstoreContext dbContext;

        public MemberDAO(FstoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public MemberDAO()
        : this(new FstoreContext())
        {
        }

        public Member CheckLogin(string email, string password)
        {
            return dbContext.Members.SingleOrDefault(m => m.Email == email && m.Password == password);
        }

        public IEnumerable<Member> GetMemberList()
        {
            
            return dbContext.Members;
            
        }

        public Member GetMemberByID(int memberId)
        {
            return dbContext.Members.SingleOrDefault(m => m.MemberId == memberId);
        }

        public void AddNew(Member member)
        {
            if (GetMemberByID(member.MemberId) == null)
            {
                dbContext.Members.Add(member);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The member is already exist.");
            }
        }

        public void Update(Member member)
        {
            var existingMember = GetMemberByID(member.MemberId);
            if (existingMember != null)
            {
                existingMember.Email = member.Email;
                existingMember.CompanyName = member.CompanyName;
                existingMember.City = member.City;
                existingMember.Country = member.Country;
                existingMember.Password = member.Password;

                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The member does not already exist.");
            }
        }

        public void Remove(int memberId)
        {
            var member = GetMemberByID(memberId);
            if (member != null)
            {
                dbContext.Members.Remove(member);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The member does not already exist.");
            }
        }
    }
}
