//全局配置文件(基于具体项目,如:PPTS)
var ppts = ppts || mcs.app;

(function () {
    ppts.name = 'ppts';
    ppts.version = '1.0';
    ppts.currentEnv = 'local';
    ppts.user = ppts.user || {};
    ppts.rootUrl = mcs.config.baseUrl;

    // 加载基地址
    switch (ppts.currentEnv) {
        case 'dev': //仅适合调试webapi,不适合加载资源
            ppts.rootUrl = 'http://localhost:1399/';
            break;
        case 'local':
        default:
            ppts.rootUrl = 'http://localhost';
            break;
        case 'test':
            ppts.rootUrl = 'http://10.1.56.80';
            break;
        case 'prod':
            ppts.rootUrl = '';
            break;
    }

    ppts.config = {
        modules: {
            dashboard: 'app/dashboard/ppts.dashboard',
            auditing: 'app/auditing/ppts.auditing',
            customer: 'app/customer/ppts.customer',
            account: 'app/account/ppts.account',
            product: 'app/product/ppts.product',
            order: 'app/order/ppts.order',
            schedule: 'app/schedule/ppts.schedule',
            infra: 'app/infra/ppts.infra',
            custcenter: 'app/custcenter/ppts.custcenter',
            contract: 'app/contract/ppts.contract',
        },
        dictMappingConfig: {
            // 公共相关
            dateRange: 'c_codE_ABBR_dateRange',
            ifElse: 'c_codE_ABBR_ifelse',

            grade: 'c_codE_ABBR_CUSTOMER_GRADE',
            gender: 'c_codE_ABBR_GENDER',
            idtype: 'c_codE_ABBR_BO_Customer_CertificateType',
            income: 'c_codE_ABBR_HOMEINCOME',
            childMale: 'c_codE_ABBR_CHILDMALEDICTIONARY',
            childFemale: 'c_codE_ABBR_CHILDFEMALEDICTIONARY',
            parentMale: 'c_codE_ABBR_PARENTMALEDICTIONARY',
            parentFemale: 'c_codE_ABBR_PARENTFEMALEDICTIONARY',
           
            source: 'c_Code_Abbr_BO_Customer_Source',
            assignment: 'c_codE_ABBR_Customer_Assign',
            valid: 'c_codE_ABBR_Customer_Valid',
            period: 'c_codE_ABBR_Period',
            contactType: 'c_codE_ABBR_Customer_CRM_NewContactType',
            // 学年
            academicYear: 'c_codE_ABBR_ACDEMICYEAR',
            // 是否复读
            studyAgain: 'c_codE_ABBR_StudyAgain',
            // vip客户类型
            vipType: 'c_codE_ABBR_CUSTOMER_VipType',
            // vip客户等级
            vipLevel: 'c_codE_ABBR_CUSTOMER_VipLevel',

            /*
            * 跟进记录相关
            */
            // 跟进类型
            followType: 'c_codE_ABBR_Customer_CRM_SaleContactType',
            // 跟进阶段
            followStage: 'c_codE_ABBR_Customer_CRM_SalePhase',
            // 沟通一级结果
            mainTask: 'c_codE_ABBR_Customer_CRM_CommunicateResultFirstEx',
            // 沟通二级结果
            subTalk: 'c_codE_ABBR_Customer_CRM_CommunicateResultSecondEx',
            // 客户级别
            customerLevel: 'c_codE_ABBR_Customer_CRM_CustomerLevelEx',
            // 无效原因
            invalidReason: 'c_codE_ABBR_Customer_CRM_InvaliCustomerType',
            // 购买意图
            purchaseIntension: 'c_codE_ABBR_Customer_CRM_PurchaseIntent',
            // 实际上门人数
            verifyPeople: 'c_codE_ABBR_Customer_CRM_RealCallPersonNum',

            /*
            * 产品相关
            */
            // 产品大类
            categoryType: 'c_codE_ABBR_Product_CategoryType',
            // 服务费类型
            expenseType: 'c_codE_ABBR_Product_ExpenseType',
            // 年级类型
            gradeType: 'c_codE_ABBR_Product_GradeType',
            // 科目类型
            subjectType: 'c_codE_ABBR_STUDENTBRANCH',
            // 科目
            subject: 'c_codE_ABBR_BO_Product_TeacherSubject',
            // 季节
            season: 'c_codE_ABBR_Product_Season',
            // 课次/课时时长
            duration: 'c_codE_ABBR_BO_ProductDuration',
            // 课程级别
            courseLevel: 'c_codE_ABBR_Product_CourseLevel',
            // 辅导类型
            coachType: 'c_codE_ABBR_Product_CoachType',
            // 班组类型
            groupType: 'c_codE_ABBR_Product_GroupType',
            // 班级类型
            classType: 'c_codE_ABBR_Product_GroupClassType',
            // 跨校区产品收入归属
            belonging: 'c_codE_ABBR_Product_IncomeBelonging',
            // 薪酬规则对象
            rule: 'c_codE_ABBR_Product_RuleObject',
            // 颗粒度
            unit: 'c_codE_ABBR_Product_ProductUnit',
            // 合作类型
            hasPartner: 'c_codE_ABBR_Product_HasPartner'
        },
        dataServiceConfig: {
            // Auditing Services
            auditingDataService: 'app/auditing/auditinglist/auditing.dataService',

            // Customer Services
            confirmdoorDataService: 'app/customer/confirmdoor/confirmdoor.dataService',
            feedbackDataService: 'app/customer/feedback/feedback.dataService',
            marketDataService: 'app/customer/market/market.dataService',
            parentsmeetingDataService: 'app/customer/parentsmeeting/parentsmeeting.dataService',
            customerDataService: 'app/customer/potentialcustomer/potentialcustomer.dataService',
            returnvisitDataService: 'app/customer/returnvisit/returnvisit.dataService',
            scoreDataService: 'app/customer/score/score.dataService',
            studentDataService: 'app/customer/student/student.dataService',
            followDataService: 'app/customer/follow/follow.dataService',
            weeklyFeedbackDataService: 'app/customer/weeklyfeedback/weeklyfeedback.dataService',

            // Account Services
            // 账户显示
            accountDisplayDataService: 'app/account/display/display.dataService',
            // 账户充值
            accountChargeDataService: 'app/account/charge/charge.dataService',
            // 账户退费
            accountRefundDataService: 'app/account/refund/refund.dataService',
            // 账户服务费扣减
            accountDeductDataService: 'app/account/deduct/deduct.dataService',
            // 账户服务费返还
            accountReturnDataService: 'app/account/return/return.dataService',
            // 账户转让
            accountTransferDataService: 'app/account/transfer/transfer.dataService',

            // Product Services
            productDataService: 'app/product/productlist/product.dataService',
            productCategoryDataService: 'app/product/productcategory/productcategory.dataService',

            // Schedule Services
            classgroupDataService: 'app/schedule/classgroup/classgroup.dataService',
            classgroupCourseDataService: 'app/schedule/classgroupcourse/classgroupcourse.dataService',
            confirmCourseDataService: 'app/schedule/confirmcourse/confirmcourse.dataService',
            settingListDataService: 'app/schedule/settinglist/settinglist.dataService',
            studentAssignmentDataService: 'app/schedule/studentassignment/studentassignment.dataService',
            studentCourseDataService: 'app/schedule/studentcourse/studentcourse.dataService',
            teacherAssignmentDataService: 'app/schedule/teacherassignment/teacherassignment.dataService',
            teacherCourseDataService: 'app/schedule/teachercourse/teachercourse.dataService',

            // Order Services
            classhourCourseDataService: 'app/order/classhour/classhour.dataService',
            purchaseCourseDataService: 'app/order/purchase/purchase.dataService',
            unsubscribeCourseDataService: 'app/order/unsubscribe/unsubscribe.dataService',

            // Infra Services
            customerDiscountDataService: 'app/infra/customerdiscount/customerdiscount.dataService',
            dictionaryDataService: 'app/infra/dictionary/dictionary.dataService',
            extraGiftDataService: 'app/infra/extragift/extragift.dataService',
            nonCustomerDiscountDataService: 'app/infra/noncustomerdiscount/noncustomerdiscount.dataService',
            servicefeeDataService: 'app/infra/servicefee/servicefee.dataService',

            // CustCenter Services
            custserviceDataService: 'app/custcenter/custservice/custservice.dataService',

            // Contract Services
            contractListDataService: 'app/contract/contractlist/contractlist.dataService',
            payListDataService: 'app/contract/paylist/paylist.dataService',
            refundListDataService: 'app/contract/refundlist/refundlist.dataService',
        },
        webportalBaseUrl: ppts.rootUrl + '/PPTSWebApp/PPTS.Portal/',
        componentBaseUrl: ppts.rootUrl + '/MCSWebApp/MCS.Web.Component/',
        customerApiBaseUrl: ppts.rootUrl + '/PPTSWebApp/PPTS.WebAPI.Customers/',
        orderApiBaseUrl: ppts.rootUrl + '/PPTSWebApp/PPTS.WebAPI.Orders/',
        productApiBaseUrl: ppts.rootUrl + '/PPTSWebApp/PPTS.WebAPI.Products/'
    };

    mcs.app.config.componentBaseUrl = ppts.config.componentBaseUrl;

    return ppts;

})();