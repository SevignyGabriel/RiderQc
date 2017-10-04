﻿using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Linq;
using System.Collections.Generic;

namespace RiderQc.Web.DAL
{
    public class UserDao : IUserDao
    {
        public bool DeleteUser(string username)
        {
            int result = -1;
            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Users.Remove(ctx.Users.FirstOrDefault(x => x.Username == username));
                result = ctx.SaveChanges();
            }

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RegisterUser(User user)
        {
            int result = -1;
            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Users.Add(user);
                result = ctx.SaveChanges();
            }

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckUserExistence(string username)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                foreach (User u in ctx.Users)
                {
                    if (u.Username == username)
                        return true;
                }

            }
            return false;
        }

        public User GetUserById(int userId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                return ctx.Users.Find(userId);
            }
        }

        public List<User> GetAllUsers() 
        {


            using (RiderQcContext ctx = new RiderQcContext())
            {
                var users = ctx.Users;

                if (users != null)
                {
                    return users.ToList();
                }
                else
                {
                    return new List<User>();
                }
            }
        }

        public List<Trajet> GetAllTrajets()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var trajets = ctx.Trajets;

                if (trajets != null)
                {
                    return trajets.ToList();
                }
                else
                {
                    return new List<Trajet>();
                }
            }
        }

        public List<Ride> GetAllRides()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var rides = ctx.Rides;

                if (rides != null)
                {
                    return rides.ToList();
                }
                else
                {
                    return new List<Ride>();
                }
            }
        }
    }
}