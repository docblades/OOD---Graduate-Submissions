using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using GraduateSubmissionsMVC.Models;
using GraduateSubmissionsMVC.Models.SysAdmin;

namespace GraduateSubmissionsMVC.Controllers
{
    public class AccountController : Controller
    {

        GraduateContext db = new GraduateContext();

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
			if(Request.IsAuthenticated)
				RedirectToAction("Index","Home");
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

					//TODO: Redirect to dashboard or show relevant dashboard on home
					/*
					 * 	string dashboard = User.IsInRole("Reviewer") ? "Reviewer" : "";
						if (User.IsInRole("Decider"))
							dashboard = "Decider";
						else if (User.IsInRole("Admin Assist"))
							dashboard = "Application";
						else if (User.IsInRole("Sys Admin"))
							dashboard = "SysAdmin";
						return RedirectToAction("Index", dashboard);
					 */
					if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
					
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogOn", "Account");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            //set up the drop down list for roles
            RolesList roleslist = new RolesList();
            ViewBag.roleslist = new SelectList(roleslist.Roles);

            ViewBag.DepartmentID = new SelectList(db.DepartmentModel, "ID", "Name");

            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                //attempt to add a role to a user
                string[] s = new string [] { model.UserName };
                Roles.AddUsersToRole(s, model.Role);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);


                    //add profile information
                    var profile = Profile.GetProfile(model.UserName);
                    profile.FirstName = model.FirstName;
                    profile.LastName = model.LastName;
                    profile.Department = (from a in db.DepartmentModel
                                         where a.ID == model.DepartmentID
                                         select a.Name).ToList()[0];
                    profile.Save();
					//Sends you to your dashboard
					string dashboard = model.Role.ToString();
					if (dashboard.Equals("Sys Admin"))
						dashboard = "SysAdmin";
					else if (dashboard.Equals("Admin Assist"))
						dashboard = "Application";
                    return RedirectToAction("Index", dashboard);
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            //set up the drop down list for roles
            RolesList roleslist = new RolesList();
            ViewBag.roleslist = new SelectList(roleslist.Roles);

            ViewBag.DepartmentID = new SelectList(db.DepartmentModel, "ID", "Name");

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        //view all the user accounts
        public ActionResult Users()
        {
            List<Users> userlist = new List<Users>();

            foreach (var item in Membership.GetAllUsers())
            {
                MembershipUser user = (MembershipUser)item;
                var profile = Profile.GetProfile(user.UserName);
                userlist.Add(new Users { EmailAddress = user.Email, UserName = user.UserName, Roles = Roles.GetRolesForUser(user.UserName), FirstName = profile.FirstName, LastName = profile.LastName, Department = profile.Department });
            }

            return View(userlist);
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
