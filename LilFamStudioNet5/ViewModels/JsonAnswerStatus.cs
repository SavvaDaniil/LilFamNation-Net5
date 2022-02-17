using LilFamStudioNet5.ViewModels.Abonement;
using LilFamStudioNet5.ViewModels.Admin;
using LilFamStudioNet5.ViewModels.Branch;
using LilFamStudioNet5.ViewModels.DanceGroup;
using LilFamStudioNet5.ViewModels.Discount;
using LilFamStudioNet5.ViewModels.PurchaseAbonement;
using LilFamStudioNet5.ViewModels.Teacher;
using LilFamStudioNet5.ViewModels.TeacherReplacement;
using LilFamStudioNet5.ViewModels.User;
using LilFamStudioNet5.ViewModels.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels
{
    public class JsonAnswerStatus
    {
        public string status { get; set; }
        public string errors { get; set; }

        public string accessToken { get; set; }
        public int id_of_user { get; set; }

        public List<TeacherLiteViewModel> teacherLiteViewModels { get; set; }
        public TeacherEditViewModel teacherEditViewModel { get; set; }

        public List<BranchLiteViewModel> branchLiteViewModels { get; set; }
        public BranchEditViewModel branchEditViewModel { get; set; }

        public List<AbonementLiteViewModel> abonementLiteViewModels { get; set; }
        public AbonementEditViewModel abonementEditViewModel { get; set; }

        public List<DanceGroupLiteViewModel> danceGroupLiteViewModels { get; set; }
        public DanceGroupEditViewModel danceGroupEditViewModel { get; set; }

        public List<DiscountLiteViewModel> discountLiteViewModels { get; set; }
        public DiscountEditViewModel discountEditViewModel { get; set; }

        public List<UserSearchPreviewViewModel> userSearchPreviewViewModels { get; set; }
        public int usersSearchCount { get; set; }
        public UserSearchPreviewViewModel userSearchPreviewViewModel { get; set; }
        public UserProfileViewModel userProfileViewModel { get; set; }

        public AbonementsBySpecialStatusViewModel abonementsBySpecialStatusViewModel { get; set; }
        public AbonementForBuyingViewModel abonementForBuyingViewModel { get; set; }

        public DanceGroupScheduleWithNameOfDayOfWeek danceGroupScheduleWithNameOfDayOfWeek { get; set; }
        public List<DanceGroupScheduleViewModel> danceGroupScheduleViewModels { get; set; }
        public List<DanceGroupScheduleWithNameOfDayOfWeek> danceGroupScheduleWithNameOfDayOfWeeks { get; set; }

        public VisitPrepareViewModel visitPrepareViewModel { get; set; }
        public VisitLiteViewModel visitLiteViewModel { get; set; }
        public List<VisitLiteViewModel> visitLiteViewModels { get; set; }
        public PurchaseAbonementLiteViewModel purchaseAbonementLiteViewModel { get; set; }
        public List<PurchaseAbonementLiteViewModel> purchaseAbonementLiteViewModels { get; set; }
        public List<DiscountWithConnectionToUserLiteViewModel> discountWithConnectionToUserLiteViewModels { get; set; }
        public List<DanceGroupConnectionToUserAdminViewModel> danceGroupConnectionToUserAdminViewModels { get; set; }
        public List<AbonementLiteWithPrivateConnectionToUserViewModel> abonementLiteWithPrivateConnectionToUserViewModels { get; set; }
        public List<PurchaseAbonementEditViewModel> purchaseAbonementEditViewModels { get; set; }
        public PurchaseAbonementEditViewModel purchaseAbonementEditViewModel { get; set; }
        
        //это в основнмо вкладка "Занятия"
        public List<DanceGroupByDanceGroupDayOfWeekLiteViewModel> danceGroupByDanceGroupDayOfWeekLiteViewModels { get; set; }
        public VisitLessonsByDateViewModel visitLessonsByDateViewModel { get; set; }

        public TeacherReplacementStatusViewModel teacherReplacementStatusViewModel { get; set; }

        public PurchaseAbonementStatistikLiteViewModel purchaseAbonementStatistikLiteViewModel { get; set; }
        public VisitStatisticLiteViewModel visitStatisticLiteViewModel { get; set; }

        public AdminSearchViewModel adminSearchViewModel { get; set; }
        public AdminEditViewModel adminEditViewModel { get; set; }


        public JsonAnswerStatus(string status, string errors)
        {
            this.status = status;
            this.errors = errors;
        }

        public JsonAnswerStatus(string status, string errors, string accessToken) : this(status, errors)
        {
            this.accessToken = accessToken;
        }

        public JsonAnswerStatus(string status, string errors, int id_of_user) : this(status, errors)
        {
            this.id_of_user = id_of_user;
        }

        public JsonAnswerStatus(string status, string errors, List<TeacherLiteViewModel> teacherLiteViewModels) : this(status, errors)
        {
            this.teacherLiteViewModels = teacherLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, TeacherEditViewModel teacherEditViewModel) : this(status, errors)
        {
            this.teacherEditViewModel = teacherEditViewModel;
        }

        public JsonAnswerStatus(string status, string errors, List<BranchLiteViewModel> branchLiteViewModels) : this(status, errors)
        {
            this.branchLiteViewModels = branchLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, BranchEditViewModel branchEditViewModel) : this(status, errors)
        {
            this.branchEditViewModel = branchEditViewModel;
        }

        public JsonAnswerStatus(string status, string errors, List<AbonementLiteViewModel> abonementLiteViewModels) : this(status, errors)
        {
            this.abonementLiteViewModels = abonementLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, AbonementEditViewModel abonementEditViewModel) : this(status, errors)
        {
            this.abonementEditViewModel = abonementEditViewModel;
        }

        public JsonAnswerStatus(string status, string errors, List<DanceGroupLiteViewModel> danceGroupLiteViewModels) : this(status, errors)
        {
            this.danceGroupLiteViewModels = danceGroupLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, DanceGroupEditViewModel danceGroupEditViewModel) : this(status, errors)
        {
            this.danceGroupEditViewModel = danceGroupEditViewModel;
        }

        public JsonAnswerStatus(string status, string errors, List<DiscountLiteViewModel> discountLiteViewModels) : this(status, errors)
        {
            this.discountLiteViewModels = discountLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, DiscountEditViewModel discountEditViewModel) : this(status, errors)
        {
            this.discountEditViewModel = discountEditViewModel;
        }

        public JsonAnswerStatus(string status, string errors, List<UserSearchPreviewViewModel> userSearchPreviewViewModels) : this(status, errors)
        {
            this.userSearchPreviewViewModels = userSearchPreviewViewModels;
        }

        public JsonAnswerStatus(string status, string errors, List<UserSearchPreviewViewModel> userSearchPreviewViewModels, int usersSearchCount) : this(status, errors)
        {
            this.userSearchPreviewViewModels = userSearchPreviewViewModels;
            this.usersSearchCount = usersSearchCount;
        }

        public JsonAnswerStatus(string status, string errors, UserSearchPreviewViewModel userSearchPreviewViewModel) : this(status, errors)
        {
            this.userSearchPreviewViewModel = userSearchPreviewViewModel;
        }

        public JsonAnswerStatus(string status, string errors, UserProfileViewModel userProfileViewModel) : this(status, errors)
        {
            this.userProfileViewModel = userProfileViewModel;
        }

        public JsonAnswerStatus(string status, string errors, AbonementsBySpecialStatusViewModel abonementsBySpecialStatusViewModel) : this(status, errors)
        {
            this.abonementsBySpecialStatusViewModel = abonementsBySpecialStatusViewModel;
        }

        public JsonAnswerStatus(string status, string errors, AbonementForBuyingViewModel abonementForBuyingViewModel) : this(status, errors)
        {
            this.abonementForBuyingViewModel = abonementForBuyingViewModel;
        }

        public JsonAnswerStatus(string status, string errors, DanceGroupScheduleWithNameOfDayOfWeek danceGroupScheduleWithNameOfDayOfWeek) : this(status, errors)
        {
            this.danceGroupScheduleWithNameOfDayOfWeek = danceGroupScheduleWithNameOfDayOfWeek;
        }

        public JsonAnswerStatus(string status, string errors, VisitLiteViewModel visitLiteViewModel) : this(status, errors)
        {
            this.visitLiteViewModel = visitLiteViewModel;
        }

        public JsonAnswerStatus(string status, string errors, DanceGroupScheduleWithNameOfDayOfWeek danceGroupScheduleWithNameOfDayOfWeek, List<VisitLiteViewModel> visitLiteViewModels) : this(status, errors, danceGroupScheduleWithNameOfDayOfWeek)
        {
            this.visitLiteViewModels = visitLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, List<DanceGroupScheduleViewModel> danceGroupScheduleViewModels) : this(status, errors)
        {
            this.danceGroupScheduleViewModels = danceGroupScheduleViewModels;
        }

        public JsonAnswerStatus(string status, string errors, List<DanceGroupScheduleWithNameOfDayOfWeek> danceGroupScheduleWithNameOfDayOfWeeks) : this(status, errors)
        {
            this.danceGroupScheduleWithNameOfDayOfWeeks = danceGroupScheduleWithNameOfDayOfWeeks;
        }

        public JsonAnswerStatus(string status, string errors, VisitPrepareViewModel visitPrepareViewModel) : this(status, errors)
        {
            this.visitPrepareViewModel = visitPrepareViewModel;
        }

        public JsonAnswerStatus(string status, string errors, List<VisitLiteViewModel> visitLiteViewModels) : this(status, errors)
        {
            this.visitLiteViewModels = visitLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, PurchaseAbonementLiteViewModel purchaseAbonementLiteViewModel) : this(status, errors)
        {
            this.purchaseAbonementLiteViewModel = purchaseAbonementLiteViewModel;
        }

        public JsonAnswerStatus(string status, string errors, List<PurchaseAbonementLiteViewModel> purchaseAbonementLiteViewModels) : this(status, errors)
        {
            this.purchaseAbonementLiteViewModels = purchaseAbonementLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, List<DiscountWithConnectionToUserLiteViewModel> discountWithConnectionToUserLiteViewModels) : this(status, errors)
        {
            this.discountWithConnectionToUserLiteViewModels = discountWithConnectionToUserLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, List<DanceGroupConnectionToUserAdminViewModel> danceGroupConnectionToUserAdminViewModels) : this(status, errors)
        {
            this.danceGroupConnectionToUserAdminViewModels = danceGroupConnectionToUserAdminViewModels;
        }

        public JsonAnswerStatus(string status, string errors, List<AbonementLiteWithPrivateConnectionToUserViewModel> abonementLiteWithPrivateConnectionToUserViewModels) : this(status, errors)
        {
            this.abonementLiteWithPrivateConnectionToUserViewModels = abonementLiteWithPrivateConnectionToUserViewModels;
        }

        public JsonAnswerStatus(string status, string errors, List<PurchaseAbonementEditViewModel> purchaseAbonementEditViewModels) : this(status, errors)
        {
            this.purchaseAbonementEditViewModels = purchaseAbonementEditViewModels;
        }

        public JsonAnswerStatus(string status, string errors, List<AbonementLiteViewModel> abonementLiteViewModels, PurchaseAbonementEditViewModel purchaseAbonementEditViewModel) : this(status, errors, abonementLiteViewModels)
        {
            this.purchaseAbonementEditViewModel = purchaseAbonementEditViewModel;
        }

        public JsonAnswerStatus(string status, string errors, List<DanceGroupByDanceGroupDayOfWeekLiteViewModel> danceGroupByDanceGroupDayOfWeekLiteViewModels) : this(status, errors)
        {
            this.danceGroupByDanceGroupDayOfWeekLiteViewModels = danceGroupByDanceGroupDayOfWeekLiteViewModels;
        }

        public JsonAnswerStatus(string status, string errors, VisitLessonsByDateViewModel visitLessonsByDateViewModel) : this(status, errors)
        {
            this.visitLessonsByDateViewModel = visitLessonsByDateViewModel;
        }

        public JsonAnswerStatus(string status, string errors, TeacherReplacementStatusViewModel teacherReplacementStatusViewModel) : this(status, errors)
        {
            this.teacherReplacementStatusViewModel = teacherReplacementStatusViewModel;
        }

        public JsonAnswerStatus(string status, string errors, PurchaseAbonementStatistikLiteViewModel purchaseAbonementStatistikLiteViewModel) : this(status, errors)
        {
            this.purchaseAbonementStatistikLiteViewModel = purchaseAbonementStatistikLiteViewModel;
        }

        public JsonAnswerStatus(string status, string errors, VisitStatisticLiteViewModel visitStatisticLiteViewModel) : this(status, errors)
        {
            this.visitStatisticLiteViewModel = visitStatisticLiteViewModel;
        }

        public JsonAnswerStatus(string status, string errors, AdminSearchViewModel adminSearchViewModel) : this(status, errors)
        {
            this.adminSearchViewModel = adminSearchViewModel;
        }

        public JsonAnswerStatus(string status, string errors, AdminEditViewModel adminEditViewModel) : this(status, errors)
        {
            this.adminEditViewModel = adminEditViewModel;
        }
    }
}
