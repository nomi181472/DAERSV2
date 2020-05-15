using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAERS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DAERS.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext Context;
        public AuthRepository(DataContext context)
        {
            Context = context;

        }
        public  async Task<User> Login(string username, string password)
        {
            var user= await Context.Users.FirstOrDefaultAsync(x=>x.UserName==username);
            if (user==null)
                return null;
            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt) )  
          {
            
             var  ComputedpasswordH=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i=0;i<ComputedpasswordH.Length;i++)
                {
                    if(ComputedpasswordH[i]!=passwordHash[i])
                    return false;
                }
          }
          return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordH,passwordS;
            CreatePasswordHash(password,out passwordH,out passwordS);
            user.PasswordHash=passwordH;
            user.PasswordSalt=passwordS;
            int Height=Convert.ToInt32(user.Height)*12;
            double H=user.Height-Convert.ToInt32(user.Height);
            Height=Height+Convert.ToInt32(H*100);
            double w=user.Weight*2.20462*703;
            user.Bmi=(float)w/(Height*Height);
            user.Category=this.NaiveBayes( user);
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
        
            return user;
        }
        private string NaiveBayes( User user)
        {
            var   total_users= this.Context.Users.CountAsync().Result;
            string[] labels={"N","M","H"};
            double[] prob_={0,0,0};
            for(int i=0;i<labels.Count();i++)
               { 
                   var a=(double)this.Context.Users.Where(p=>p.Category==String.Format("{0}",labels[i])).CountAsync().Result;
                   prob_[i]=a/total_users;
               }
            var userBmi=this.GetBmiLabel(user.Bmi);
            var userAge=this.GetAgeLabel(user.Age);
            var userWaistLats=this.GetWaistLabel(user.Lats-user.Waist);
            double[] p_={0,0,0};
            for(int i=0;i<labels.Count();i++)
            {
                var Age_BMi=this.Context.Users.FromSql(String.Format("Select * from Users where Category=="+"'{0}'",labels[i]) )
                .Select(p=>new{p.Age,p.Bmi}).ToArray();
                var dict=this.FindLabel(this.Context.Users.FromSql(String.Format("Select * from Users where Category=="+"'{0}'" ,labels[i]))
                .Select(p=>p.Lats-p.Waist).ToArray()
                ,Age_BMi.Select(p=>p.Age).ToArray()
                ,Age_BMi.Select(p=>p.Bmi).ToArray());
                double p_lats_waist=(double)dict["Waist"][userWaistLats]/Age_BMi.Count();
                double p_Age=(double)dict["Age"][userAge]/Age_BMi.Count();
                double p_Bmi=(double)dict["Bmi"][userBmi]/Age_BMi.Count();
                p_[i]=p_lats_waist*p_Age*p_Bmi*prob_[i];
            }
            for(int i=0;i<labels.Count();i++)
                if(p_[i]==p_.Max())
                return labels[i];
            return "UnLabeled";
        }
        private Dictionary<string,Dictionary<string,int>> FindLabel(float[] warray,int[] aarray,float[] barray)
        {
            
            Dictionary<string,int> WaistLabel=new Dictionary<string, int>(){
                {"-12To0",0},
                {"0To4",0},
                {"4To8",0},
                {"8To12",0}

            };
            Dictionary<string,int> ageLabel=new Dictionary<string, int>(){
                {"18To30",0},
                {"30To45",0},
                {"45To60",0},
                {"60To70",0}
            };
            Dictionary<string,int> bmiLabel=new Dictionary<string, int>(){
                {"12To18",0},
                {"18To25",0},
                {"25To30",0},
                {"30To35",0}
            };
            Dictionary<string,Dictionary<string,int>> Data=new Dictionary<string, Dictionary<string, int>>();
            for(int i=0;i<warray.Count();i++)
                if(warray[i]>=-12 &&  warray[i]<=12)
                    WaistLabel[this.GetWaistLabel(warray[i])]++;
            for(int i=0;i<aarray.Count();i++)
                if(aarray[i]>=18 && aarray[i]<=70)
                    ageLabel[this.GetAgeLabel(aarray[i])]++;  
            for(int i=0;i<barray.Count();i++)
                if(barray[i]>=12 && barray[i]<=35)
                    bmiLabel[this.GetBmiLabel(barray[i])]++;
                
            Data.Add("Waist",WaistLabel);
            Data.Add("Age",ageLabel);
            Data.Add("Bmi",bmiLabel);
            return Data;
            

        }
        private string GetBmiLabel(double Bmi){
            if(Bmi>=12&&Bmi<18)
            return"12To18";
            if(Bmi>=18&&Bmi<25)
            return"18To25";
            if(Bmi>=25&&Bmi<30)
            return"25To30";
            return"30To35";
        }
        private string GetAgeLabel(int age){
            if(age>=18 && age<30)
            return "18To30";
            if(age>=30 && age<45)
            return"30To45";
            if(age>=45 && age<60)
            return"45To60";
            return"60To70";
        }
        private string GetWaistLabel(double value)
        {
            if (value<0)
            return "-12To0";
            if(value>=0 && value<4)
            return "0To4";
            if(value>=4 && value<8)
            return "4To8";
            return "8To12";
        }




        private void CreatePasswordHash(string password, out byte[] passwordH, out byte[] passwordS)
        {
          using(var hmac=new System.Security.Cryptography.HMACSHA512() )  
          {
              passwordS=hmac.Key;
              passwordH=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
          }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await Context.Users.AnyAsync(x=>x.UserName==username))
            return true;
            return false;
        }
    }
}