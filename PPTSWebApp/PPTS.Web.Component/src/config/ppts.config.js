//全局配置文件(基于具体项目,如:PPTS)
var ppts = ppts || mcs.app;

(function () {
    ppts.name = 'ppts';
    ppts.version = '1.0';
    ppts.user = ppts.user || {};
    ppts.enum = ppts.enum || {};

    ppts.config = {
        pageSizeItem: 20, // Datatable每页的数量
        taskDisplayItem: 5, // 待办任务在右上角显示的数量
        taskQueryInterval: 30 * 1000, // 待办任务轮询的周期(半分钟)
        datePickerFormat: 'yyyy-mm-dd',
        datetimePickerFormat: 'yyyy-mm-dd hh:ii:00',
        datePickerLang: 'zh-CN',
        modules: {
            dashboard: 'app/dashboard/ppts.dashboard',
            task: 'app/task/ppts.task',
            customer: 'app/customer/ppts.customer',
            account: 'app/account/ppts.account',
            product: 'app/product/ppts.product',
            order: 'app/order/ppts.order',
            schedule: 'app/schedule/ppts.schedule',
            infra: 'app/infra/ppts.infra',
            custcenter: 'app/custcenter/ppts.custcenter',
            query: 'app/query/ppts.query',
        },
        dictMappingConfig: {
            // 公共相关
            region: 'c_codE_ABBR_LOCATION',
            grade: 'c_codE_ABBR_CUSTOMER_GRADE',
            gender: 'c_codE_ABBR_GENDER',
            idtype: 'c_codE_ABBR_BO_Customer_CertificateType',
            income: 'c_codE_ABBR_HOMEINCOME',
            childMale: 'c_codE_ABBR_CHILDMALEDICTIONARY',
            childFemale: 'c_codE_ABBR_CHILDFEMALEDICTIONARY',
            parentMale: 'c_codE_ABBR_PARENTMALEDICTIONARY',
            parentFemale: 'c_codE_ABBR_PARENTFEMALEDICTIONARY',
            parent: 'c_codE_ABBR_PARENTDICTIONARY',
            child: 'c_codE_ABBR_CHILDDICTIONARY',
            source: 'c_codE_ABBR_BO_Customer_Source',
            contactType: 'c_codE_ABBR_Customer_CRM_NewContactType',
            // 学年
            academicYear: 'c_codE_ABBR_ACDEMICYEAR',
            // vip客户类型
            vipType: 'c_codE_ABBR_CUSTOMER_VipType',
            // vip客户等级
            vipLevel: 'c_codE_ABBR_CUSTOMER_VipLevel',

            /*
            * 学员相关
            */
            studentType: 'c_codE_ABBR_Student_Type',
            studentValid: 'c_codE_ABBR_Student_Valid',
            studentAttend: 'c_codE_ABBR_Student_Attend',
            studentCancel: 'c_codE_ABBR_Student_Cancel',
            studentSuspend: 'c_codE_ABBR_Student_Suspend',
            studentCompleted: 'c_codE_ABBR_Student_Completed',
            studentAttendRange: 'c_codE_ABBR_Student_Attend_Range',
            studentRange: 'c_codE_ABBR_Student_Range',
            sendEmailSMS: 'c_codE_ABBR_Student_SendEmailSMS',
            changeTeacherReason: 'C_Code_Abbr_BO_Customer_ChangeTeacherReason',
            //停课休学原因
            stopAlertReason: 'c_codE_ABBR_Customer_StopAlertReason',
            //退费预警原因
            refundAlertReason: 'c_codE_ABBR_Customer_RefundAlertReason',
            //停课休学类型
            stopAlertType: 'c_codE_ABBR_Customer_StopAlertType',
            //退费预警状态
            refundAlertStatus: 'c_codE_ABBR_Customer_RefundAlertStatus',
            changeTeacherReason: 'c_codE_ABBR_BO_Customer_ChangeTeacherReason',
            // 分配教师操作类型
            teacherApplyType: 'c_codE_ABBR_Customer_Teacher_ApplyType',
            // 高三毕业库学员
            graduated: 'c_codE_ABBR_Customer_Graduated',
            // 距最后上课时长
            lastCourseType: 'c_codE_ABBR_Customer_LastCourseType',

            /*
            * 教学服务会相关
            */
            meetingType: 'c_codE_ABBR_Customer_CRM_MainServiceMeeting',
            satisfaction: 'c_codE_Abbr_BO_Customer_Satisfaction',
            meetingEvent: 'c_codE_ABBR_MeetingEvent',
            meetingTitle: 'c_codE_ABBR_MeetingTitle',
            participants: 'c_codE_ABBR_Customer_CRM_MeetingObject',
            contentType: 'c_codE_ABBR_ContentType',

            /*
            * 学大反馈相关
            */
            replyType: 'c_codE_ABBR_Customer_ReplyType',
            replyObject: 'c_codE_ABBR_Customer_ReplyObject',

            /*
            * 账户相关
            */
            recordType: 'c_codE_ABBR_account_RecordType',
            //账户类型
            accountType: 'c_codE_ABBR_account_AccountType',
            //账户状态
            accountStatus: 'c_codE_ABBR_account_AccountStatus',
            //转让类型
            accountTransferType: 'c_codE_ABBR_account_TransferType',
            //缴费类型
            chargeType: 'c_codE_ABBR_account_ChargeType',
            //缴费单审核状态
            chargeAuditStatus: 'c_codE_ABBR_Account_ChargeAuditStatus',
            //退费类型
            refundType: 'c_codE_ABBR_account_RefundType',
            //退费确认操作
            refundVerifyAction: 'c_codE_ABBR_account_RefundVerifyAction',
            //退费确认状态
            refundVerifyStatus: 'c_codE_ABBR_account_RefundVerifyStatus',
            //制度外退费类型
            extraRefundType: 'c_codE_ABBR_account_ExtraRefundType',
            //支付状态
            payStatus: 'c_codE_ABBR_account_PayStatus',
            //支付类型
            payType: 'c_codE_ABBR_account_PayType',
            //发票状态
            invoiceStatus: 'c_codE_ABBR_Account_InvoiceStatus',
            //发票状态
            invoiceRecordStatus: 'c_codE_ABBR_Account_InvoiceRecordStatus',

            /*
            * 成绩相关
            */
            // 家长满意度
            scoreSatisficing: 'c_codE_ABBR_Score_Satisficing',
            // 学年度
            studyYear: 'c_codE_ABBR_Customer_StudyYear',
            // 学期
            studyTerm: 'c_codE_ABBR_Customer_StudyTerm',
            // 录取院校类型
            admissionType: 'c_codE_ABBR_Customer_AdmissionType',
            // 考试学段
            studyStage: 'c_codE_ABBR_Customer_StudyStage',
            // 考试类别
            scoreType: 'c_Code_Abbr_BO_Customer_GradeTypeExt',
            // 考试学员类别
            examCustomerType: 'c_codE_ABBR_Exam_Customer_Type',
            // 考试科目
            examSubject: 'c_codE_ABBR_Customer_Exam_Subject',
            // 考试月份
            examMonth: 'c_codE_ABBR_Exam_Month',
            // 成绩升降
            scoreChangeType: 'c_codE_ABBR_Exam_ScoreChangeType',
            // 任课教师
            scoreTeacher: 'c_codE_ABBR_scoreTeacher',
            // 教师所在学科组
            scoreTeacherOrgName: 'c_codE_ABBR_scoreTeacherOrgName',

            /*
            * 通用
            */
            //申请状态
            applyStatus: 'c_codE_ABBR_common_ApplyStatus',
            //对账状态
            checkStatus: 'c_codE_ABBR_common_CheckStatus',
            //教师类型
            teacherType: 'c_codE_ABBR_common_TeacherType',
            //打印状态
            printStatus: 'c_codE_ABBR_common_PrintStatus',
            //岗位状态
            jobStatus: 'c_codE_ABBR_common_JobStatus',
            //岗位类型
            jobType: 'c_codE_ABBR_common_JobType',
            //组织机构类型
            orgType: 'c_codE_ABBR_common_OrgType',

            /*
            * 跟进记录相关
            */
            // 跟进类型
            followType: 'c_codE_ABBR_Customer_CRM_SaleContactType',
            // 跟进阶段
            followStage: 'c_codE_ABBR_Customer_CRM_SalePhase',
            // 沟通一级结果
            mainTalk: 'c_codE_ABBR_Customer_CRM_CommunicateResultFirstEx',
            // 沟通二级结果
            subTalk: 'c_codE_ABBR_Customer_CRM_CommunicateResultSecondEx',
            // 客户级别
            customerLevel: 'c_codE_ABBR_Customer_CRM_CustomerLevelEx',
            // 无效原因
            invalidReason: 'c_codE_ABBR_Customer_CRM_InvaliCustomerType',
            // 购买意图
            purchaseIntention: 'c_codE_ABBR_Customer_CRM_PurchaseIntent',
            // 实际上门人数
            verifyPeople: 'c_codE_ABBR_Customer_CRM_RealCallPersonNum',
            // 跟进对象
            followObject: 'c_codE_ABBR_Customer_CRM_SaleContactTarget',
            //跟进人员关系
            verifyRelation: 'c_codE_ABBR_RealCallPersonRelation',

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
            hasPartner: 'c_codE_ABBR_Product_HasPartner',
            // 产品状态/销售状态
            productStatus: 'c_codE_ABBR_Product_ProductStatus',


            /*
            * 排课相关
            */
            assignCondition: 'c_codE_ABBR_AssignCondition',
            asset: 'c_codE_ABBR_Asset',
            teacher: 'c_codE_ABBR_Teacher',
            hour: 'c_codE_ABBR_Hour',
            minute: 'c_codE_ABBR_Minute',
            copyCourseType: 'c_codE_ABBR_copyCourseType',
            classStatus: 'c_codE_ABBR_Order_ClassStatus',
            lessonStatus: 'c_codE_ABBR_Order_LessonStatus',
            editCourseType: 'c_codE_ABBR_EditCourseType',
            assignStatus: 'c_codE_ABBR_Course_AssignStatus',
            startLessons: 'c_codE_ABBR_Order_StartLessons',
            endLessons: 'c_codE_ABBR_Order_EndLessons',
            student: 'c_codE_ABBR_Student',
            subGrade: 'c_codE_ABBR_SubGrade',
            subSubject: 'c_codE_ABBR_SubSubject',
            assignSource: 'c_codE_ABBR_Assign_Source',
            /*课时数，补录课时用*/
            courseAmount: 'c_codE_ABBR_CourseAmount',
            /*教师年龄区间，按教师排课用*/
            ageCollection: 'c_codE_ABBR_AgeCollection',

            /*
            * 订单相关
            */
            //订单状态
            orderStatus: 'c_codE_ABBR_Order_OrderStatus',
            //操作人岗位
            post: 'c_codE_ABBR_Order_Post',
            //特殊折扣原因
            orderSpecialType: 'c_codE_ABBR_Order_SpecialType',
            //订单类型
            orderType: 'c_codE_ABBR_Order_OrderType',
            consumeType: 'c_codE_ABBR_Order_ConsumeType',

            /*
            * 客服相关
            */
            serviceType: 'c_codE_ABBR_customer_ServiceType',
            serviceStatus: 'c_codE_ABBR_customer_ServiceStatus',
            acceptLimit: 'c_codE_ABBR_customer_AcceptLimit',
            consultType: 'c_codE_ABBR_customer_ConsultType',
            complaintTimes: 'c_codE_ABBR_customer_ComplaintTimes',
            complaintLevel: 'c_codE_ABBR_customer_ComplaintLevel',
            complaintUpgrade: 'c_codE_ABBR_customer_ComplaintUpgrade',

            /*回访相关*/
            visitType: 'c_codE_ABBR_BO_Customer_ReturnInfoType',
            visitWay: 'c_codE_ABBR_Customer_CRM_ReturnWay',
            satisficing: 'c_codE_ABBR_BO_Customer_Satisfaction',
            timeType: 'c_codE_ABBR_TimeType_Service',

            /*基础数据*/
            discountStatus: 'c_codE_ABBR_BO_Infra_DiscountStatus',
            presentStatus: 'c_codE_ABBR_BO_Infra_PresentStatus',
            campusUseInfo: 'c_codE_ABBR_BO_Infra_CampusUseInfo',
            /*综合服务费*/
            serviceFeeType: 'c_codE_ABBR_ServiceFee_ServiceType'
        },
        dataServiceConfig: {
            // Task Services
            taskDataService: 'app/task/task.dataService',

            // Customer Services
            customerService: 'app/customer/ppts.customer.service',
            customerVerifyDataService: 'app/customer/customerverify/customerverify.dataService',
            feedbackDataService: 'app/customer/feedback/feedback.dataService',
            marketDataService: 'app/customer/market/market.dataService',
            customerMeetingDataService: 'app/customer/customermeeting/customermeeting.dataService',
            customerDataService: 'app/customer/potentialcustomer/potentialcustomer.dataService',
            customerVisitDataService: 'app/customer/customervisit/customervisit.dataService',
            scoreDataService: 'app/customer/score/score.dataService',
            studentDataService: 'app/customer/student/student.dataService',
            followDataService: 'app/customer/follow/follow.dataService',
            weeklyFeedbackDataService: 'app/customer/weeklyfeedback/weeklyfeedback.dataService',
            stopAlertDataService: 'app/customer/stopalerts/stopalerts.dataService',
            refundAlertDataService: 'app/customer/refundalerts/refundalerts.dataService',
            discountDataService: 'app/infra/discount/discount.dataService',

            // Account Services
            //账户公用
            accountService: 'app/account/ppts.account.service',
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
            presentDataService: 'app/infra/present/present.dataService',
            nonCustomerDiscountDataService: 'app/infra/noncustomerdiscount/noncustomerdiscount.dataService',
            servicefeeDataService: 'app/infra/servicefee/servicefee.dataService',

            // CustCenter Services
            custserviceDataService: 'app/custcenter/custservice/custservice.dataService',

            // Query Services
            accountQueryService: 'app/query/account/account-query.service',
            assignQueryService: 'app/query/assign/assign-query.service',
            orderQueryService: 'app/query/order/order-query.service',
            studentQueryService: 'app/query/student/student-query.service',
            taskQueryService: 'app/query/task/task-query.service'
        },
        sidebarMenusConfig: [{
            name: '首页', icon: 'home', route: 'ppts.dashboard', active: true
        }, {
            name: '审批管理', icon: 'check-square-o',
            children: [
                { name: '待办列表', route: 'ppts.userTask' },
                { name: '已办列表', route: 'ppts.completedTask' },
                { name: '通知列表', route: 'ppts.notify' },
                { name: '流程表单', route: 'ppts.workflow' }
            ]
        }, {
            name: '客户管理', icon: 'user',
            permission: '潜客管理,潜客管理-本部门,潜客管理-本校区,潜客管理-本分公司,潜客管理-全国,市场资源,市场资源-本部门,市场资源-本校区,市场资源-本分公司,市场资源-全国,跟进管理（学员视图-跟进记录跟进记录详情）,跟进管理（学员视图-跟进记录跟进记录详情）-本部门,跟进管理（学员视图-跟进记录跟进记录详情）-本校区,跟进管理（学员视图-跟进记录跟进记录详情）-本分公司,跟进管理（跟进记录详情）-全国,上门管理,上门管理-本部门,上门管理-本校区,上门管理-本分公司,上门管理-全国,学员管理,学员管理-本部门,学员管理-本校区,学员管理-本分公司,学员管理-全国,回访管理（学员视图-回访、回访详情）,回访管理（学员视图-回访、回访详情）-本部门,回访管理（学员视图-回访、回访详情）-本校区,回访管理（学员视图-回访、回访详情）-本分公司,回访管理（学员视图-回访、回访详情）全国,成绩管理（学员视图-成绩）,成绩管理（学员视图-成绩、成绩详情）-本部门,成绩管理（学员视图-成绩、成绩详情）-本校区,成绩管理（学员视图-成绩、成绩详情）-本分公司,成绩管理（学员视图-成绩、成绩详情）-全国,教学服务会管理（学员视图-教学服务会、详情查看）,教学服务会管理（学员视图-教学服务会、详情查看）-本部门,教学服务会管理（学员视图-教学服务会、详情查看）-本校区,教学服务会管理（学员视图-教学服务会、详情查看）-本分公司,教学服务会管理（学员视图-教学服务会、详情查看）-全国,学大反馈管理（学员视图-家校互动）,学大反馈管理（学员视图-家校互动）-本部门,学大反馈管理（学员视图-家校互动）-本校区,学大反馈管理（学员视图-家校互动）-本分公司,学大反馈管理（学员视图-家校互动）-全国',
            children: [
               { name: '潜客管理', route: 'ppts.customer', permission: '潜客管理,潜客管理-本部门,潜客管理-本校区,潜客管理-本分公司,潜客管理-全国' },
               { name: '市场资源', route: 'ppts.market', permission: '市场资源,市场资源-本部门,市场资源-本校区,市场资源-本分公司,市场资源-全国' },
               { name: '跟进管理', route: 'ppts.follow', permission: '跟进管理（学员视图-跟进记录跟进记录详情）,跟进管理（学员视图-跟进记录跟进记录详情）-本部门,跟进管理（学员视图-跟进记录跟进记录详情）-本校区,跟进管理（学员视图-跟进记录跟进记录详情）-本分公司,跟进管理（跟进记录详情）-全国' },
               { name: '上门管理', route: 'ppts.customerverify', permission: '上门管理,上门管理-本部门,上门管理-本校区,上门管理-本分公司,上门管理-全国' },
               { name: '学员管理', route: 'ppts.student', permission: '学员管理,学员管理-本部门,学员管理-本校区,学员管理-本分公司,学员管理-全国' },
               { name: '回访管理', route: 'ppts.customervisit', permission: '回访管理（学员视图-回访、回访详情）,回访管理（学员视图-回访、回访详情）-本部门,回访管理（学员视图-回访、回访详情）-本校区,回访管理（学员视图-回访、回访详情）-本分公司,回访管理（学员视图-回访、回访详情）全国' },
               { name: '成绩管理', route: 'ppts.score', permission: '成绩管理（学员视图-成绩）,成绩管理（学员视图-成绩、成绩详情）-本部门,成绩管理（学员视图-成绩、成绩详情）-本校区,成绩管理（学员视图-成绩、成绩详情）-本分公司,成绩管理（学员视图-成绩、成绩详情）-全国' },
               { name: '教学服务会', route: 'ppts.customermeeting', permission: '教学服务会管理（学员视图-教学服务会、详情查看）,教学服务会管理（学员视图-教学服务会、详情查看）-本部门,教学服务会管理（学员视图-教学服务会、详情查看）-本校区,教学服务会管理（学员视图-教学服务会、详情查看）-本分公司,教学服务会管理（学员视图-教学服务会、详情查看）-全国' },
               { name: '学大反馈', route: 'ppts.feedback', permission: '学大反馈管理（学员视图-家校互动）,学大反馈管理（学员视图-家校互动）-本部门,学大反馈管理（学员视图-家校互动）-本校区,学大反馈管理（学员视图-家校互动）-本分公司,学大反馈管理（学员视图-家校互动）-全国' }
            ]
        }, {
            name: '缴费管理', icon: 'credit-card',
            permission: '缴费单管理（缴费单详情）,缴费单管理（缴费单详情）-本部门,缴费单管理（缴费单详情）-本校区,缴费单管理（缴费单详情）-本分公司,缴费单管理（缴费单详情）-全国,收款管理,收款管理-本部门,收款管理-本校区,收款管理-本分公司,收款管理-全国,退费管理,退费管理-本部门,退费管理-本校区,退费管理-本分公司,退费管理-全国',
            children: [
               { name: '缴费单管理', route: 'ppts.accountCharge-query', permission: '缴费单管理（缴费单详情）,缴费单管理（缴费单详情）-本部门,缴费单管理（缴费单详情）-本校区,缴费单管理（缴费单详情）-本分公司,缴费单管理（缴费单详情）-全国' },
               { name: '收款管理', route: 'ppts.accountChargePayment-query', permission: '收款管理,收款管理-本部门,收款管理-本校区,收款管理-本分公司,收款管理-全国' },
               { name: '退费管理', route: 'ppts.accountRefund-query', permission: '退费管理,退费管理-本部门,退费管理-本校区,退费管理-本分公司,退费管理-全国' }
            ]
        }, {
            name: '订购管理', icon: 'shopping-bag',
            permission: '订购管理列表（订购单详情）,订购管理列表（订购单详情）-本部门,订购管理列表（订购单详情）-本校区,订购管理列表（订购单详情）-本分公司,订购管理列表（订购单详情）-全国,退订管理列表,退订管理列表-本部门,退订管理列表-本校区,退订管理列表-本分公司,退订管理列表-全国',
            children: [
               { name: '订购列表', route: 'ppts.purchase', permission: '订购管理列表（订购单详情）,订购管理列表（订购单详情）-本部门,订购管理列表（订购单详情）-本校区,订购管理列表（订购单详情）-本分公司,订购管理列表（订购单详情）-全国' },
               { name: '退订列表', route: 'ppts.unsubscribe', permission: '退订管理列表,退订管理列表-本部门,退订管理列表-本校区,退订管理列表-本分公司,退订管理列表-全国' }
            ]
        }, {
            name: '排课管理', icon: 'calendar',
            permission: '客户课表管理列表（打印课表）,客户课表管理列表（打印课表）-本部门,客户课表管理列表（打印课表）-本校区,客户课表管理列表（打印课表）-本分公司,客户课表管理列表（打印课表）-全国,按学员排课-本校区,按教师排课-本校区,教师课表,教师课表-本部门,教师课表-本校区,教师课表-本分公司,教师课表-全国,班级管理（按钮查看班级、按钮查看学生）-本校区,班级管理（按钮查看班级、按钮查看学生）-本分公司,班级管理（按钮查看班级、按钮查看学生）-全国,教师上课记录（打印上课记录）,教师上课记录（打印上课记录）-本部门,教师上课记录（打印上课记录）-本校区,教师上课记录（打印上课记录）-本分公司,教师上课记录（打印）-全国,按学员排课-本校区,按教师排课-本校区,班级管理（按钮查看班级、按钮查看学生）-本校区,班级管理（按钮查看班级、按钮查看学生）-本分公司,班级管理（按钮查看班级、按钮查看学生）-全国,教师课时量查询,教师上课记录（打印上课记录）,教师上课记录（打印上课记录）-本部门,教师上课记录（打印上课记录）-本校区,教师上课记录（打印上课记录）-本分公司,教师上课记录（打印）-全国,教师查看-教师课表',
            children: [
               { name: '客户课表', route: 'ppts.studentcourse', permission: '客户课表管理列表（打印课表）,客户课表管理列表（打印课表）-本部门,客户课表管理列表（打印课表）-本校区,客户课表管理列表（打印课表）-本分公司,客户课表管理列表（打印课表）-全国' },
               { name: '按学员排课', route: 'ppts.schedule', permission: '按学员排课-本校区' },
               { name: '按教师排课', route: 'ppts.schedule-tchasgmt', permission: '按教师排课-本校区' },
               { name: '教师课表打印', route: 'ppts.teachercourse', permission: '教师课表,教师课表-本部门,教师课表-本校区,教师课表-本分公司,教师课表-全国' },
               { name: '班级管理', route: 'ppts.classgroup', permission: '班级管理（按钮查看班级、按钮查看学生）-本校区,班级管理（按钮查看班级、按钮查看学生）-本分公司,班级管理（按钮查看班级、按钮查看学生）-全国' },
               { name: '教师课时量', route: 'ppts.classgroupcourse', permission: '教师课时量查询' },
               { name: '教师上课记录', route: 'ppts.teachercourserecord', permission: '教师上课记录（打印上课记录）,教师上课记录（打印上课记录）-本部门,教师上课记录（打印上课记录）-本校区,教师上课记录（打印上课记录）-本分公司,教师上课记录（打印）-全国' },
               { name: '教师个人课表', route: 'ppts.tchcoursepsn', permission: '教师查看-教师课表' }
            ]
        }, {
            name: '产品管理', icon: 'suitcase',
            permission: '产品管理列表（产品详情）,产品管理列表（产品详情）-本校区,产品管理列表（产品详情）-本分公司,产品管理列表（产品详情）-全国',
            children: [
               { name: '产品列表', route: 'ppts.product', permission: '产品管理列表（产品详情）,产品管理列表（产品详情）-本校区,产品管理列表（产品详情）-本分公司,产品管理列表（产品详情）-全国' }
            ]
        }, {
            name: '基础数据', icon: 'gear',
            permission: '字典管理,折扣表管理-本分公司,折扣表管理-全国,综合服务费管理-本分公司,综合服务费管理-全国,买赠表管理-本分公司,买赠表管理-全国,校区维护-本分公司,信息来源维护',
            children: [
               { name: '字典管理', route: 'ppts.dictionary', permission: '字典管理' },
               { name: '折扣表管理', route: 'ppts.discount', permission: '折扣表管理-本分公司,折扣表管理-全国' },
               { name: '综合服务费管理', route: 'ppts.servicefee', permission: '综合服务费管理-本分公司,综合服务费管理-全国' },
               { name: '买赠表管理', route: 'ppts.present', permission: '买赠表管理-本分公司,买赠表管理-全国' },
               { name: '校区维护', route: 'ppts.school', permission: '校区维护-本分公司' },
               { name: '信息来源维护', route: 'ppts.source', permission: '信息来源维护' }
            ]
        }, {
            name: '客服中心', icon: 'microphone',
            permission: '客户服务管理（客服详情）,客户服务管理（客服详情）-本部门,客户服务管理（客服详情）-本校区,客户服务管理（客服详情）-本分公司,客户服务管理（客服详情）-全国',
            children: [
               { name: '客户服务', route: 'ppts.custservice', permission: '客户服务管理（客服详情）,客户服务管理（客服详情）-本部门,客户服务管理（客服详情）-本校区,客户服务管理（客服详情）-本分公司,客户服务管理（客服详情）-全国' }
            ]
        }, {
            name: '综合查询', icon: 'search',
            children: [
              { name: '全部待办查询', route: 'ppts.query-task', permission: '' },
               { name: '学员信息列表查询', route: 'ppts.query-student-list', permission: '' },
               { name: '缴费单查询', route: 'ppts.query-charge', permission: '' },
               { name: '收款列表查询', route: 'ppts.query-payment', permission: '' },
               { name: '退款列表查询', route: 'ppts.query-refund', permission: '' },
               { name: 'POS刷卡列表查询', route: 'ppts.query-posrecord', permission: '' },
               { name: '班组班级列表查询', route: 'ppts.query-class-group', permission: '' },
               { name: '学员课时量查询', route: 'ppts.query-student-course', permission: '' },
               { name: '教师课时量查询', route: 'ppts.query-teacher-course', permission: '' },
               { name: '订购列表查询', route: 'ppts.query-order-list', permission: '' },
               { name: '课时兑换列表查询', route: 'ppts.query-order-exchange', permission: '' },
               { name: '库存单价查询', route: 'ppts.query-order-stock', permission: '' }
            ]
        }]
    };

    // 读取配置文件
    var initConfigSection = function (section) {
        if (section) {
            for (var item in section) {
                ppts.config[item] = section[item];
            }
        }
    }

    var initConfig = function () {
        window.onload = function () {
            //var configData = sessionStorage.getItem('configData');
            //if (!configData) {
            //    var parameters = document.getElementById('configData');
            //    sessionStorage.setItem('configData', parameters.value);
            //    configData = sessionStorage.getItem('configData');
            //    parameters.value = '';
            //}

            if (document.getElementById('configData').value) {
                var config = JSON.parse(document.getElementById('configData').value);

                initConfigSection(config.pptsWebAPIs);

                mcs.app.config.timeStamp = config.timestamp;

                mcs.app.config.mcsComponentBaseUrl = ppts.config.mcsComponentBaseUrl;
                mcs.app.config.pptsComponentBaseUrl = ppts.config.pptsComponentBaseUrl;
            }

            loadAssets();
        };
    };

    var loadAssets = function () {
        mcs.g.loadCss({
            cssFiles: [
                //<!--#bootstrap基础样式-->
                'libs/bootstrap-3.3.5/css/bootstrap',
                //<!--#datetime组件样式-->
                'libs/date-time-3.0.0/css/bootstrap-datetimepicker',
                //<!--#ztree组件样式-->
                'libs/zTree-3.5.22/css/metroStyle/metroStyle',
                //<!--#autocomplete组件样式-->
                'libs/ng-tags-input-3.0.0/ng-tags-input',
                //<!--#font awesome字体样式-->
                'libs/font-awesome-4.5.0/css/font-awesome.min',
                //<!--#ace admin基础样式-->
                'libs/ace-1.3.1/css/ace.min',
                'libs/ace-1.3.1/css/ace-fonts',
                //<!--#blockUI样式-->
                'libs/angular-block-ui-0.2.2/dist/angular-block-ui',
                 //<!--#日程插件的样式-->
                'libs/fullcalendar-2.6.1/fullcalendar',
                //<!--#全局组件样式-->
                'libs/mcs-jslib-1.0.0/component/mcs.component',
                //<!--下拉框样式-->
                'libs/angular-ui-select-0.13.2/dist/select2',
                'libs/angular-dialog-service-5.3.0/dist/dialogs.min',
                //<!--消息提示框-->
                'libs/gritter-1.7.4/jquery.gritter'
            ], localCssFiles: [
                //<!--#网站主样式-->
                'assets/styles/site'
            ]
        });

        mcs.g.loadRequireJs(
            'libs/requirejs-2.1.22/require',
            mcs.app.config.pptsComponentBaseUrl + 'build/require.config.min');
    };

    initConfig();

    return ppts;

})();
