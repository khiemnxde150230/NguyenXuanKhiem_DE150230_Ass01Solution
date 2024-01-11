using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {

        public void DeleteMember(int memberId)
        {
            MemberDAO.Instance.Remove(memberId);
        }

        public Member GetMemberById(int memberId)
        {
            return MemberDAO.Instance.GetMemberByID(memberId);
        }

        public IEnumerable<Member> GetMembers()
        {
            return MemberDAO.Instance.GetMemberList();
        }

        public void InsertMember(Member member)
        {
            MemberDAO.Instance.AddNew(member);
        }

        public void UpdateMember(Member member)
        {
            MemberDAO.Instance.Update(member);
        }
        public Member CheckLogin(string email, string password)
        {
            return MemberDAO.Instance.CheckLogin(email, password);
        }
    }
}
