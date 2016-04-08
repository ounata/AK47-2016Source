// 年级过滤器
ppts.ng.filter('grade', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.grade], current);
    };
});
// 性别过滤器
ppts.ng.filter('gender', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.gender], current);
    };
});
// 证件类别过滤器
ppts.ng.filter('idtype', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.idtype], current);
    };
});
// 家庭总收入过滤器
ppts.ng.filter('income', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.income], current);
    };
});
// 孩子(男)亲属关系过滤器
ppts.ng.filter('childMale', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.childMale], current);
    };
});
// 孩子(女)亲属关系过滤器
ppts.ng.filter('childFemale', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.childFemale], current);
    };
});
// 家长(男)亲属关系过滤器
ppts.ng.filter('parentMale', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.parentMale], current);
    };
});
// 家长(女)亲属关系过滤器
ppts.ng.filter('parentFemale', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.parentFemale], current);
    };
});
// 学年过滤器
ppts.ng.filter('academicYear', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.academicYear], current);
    };
});
// vip客户类型过滤器
ppts.ng.filter('vipType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.vipType], current);
    };
});
// 客户级别过滤器
ppts.ng.filter('vipLevel', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.vipLevel], current);
    };
});
// 是否复读过滤器
ppts.ng.filter('studyAgain', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.studyAgain], current);
    };
});
// 信息来源过滤器
ppts.ng.filter('source', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.source], current);
    };
});
// 是否分配咨询师/坐席/市场专员过滤器
ppts.ng.filter('assignment', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.assignment], current);
    };
});
// 是否有效客户过滤器
ppts.ng.filter('valid', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.valid], current);
    };
});
// 跟进类型过滤器
ppts.ng.filter('followType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.followType], current);
    };
});
// 跟进阶段过滤器
ppts.ng.filter('followStage', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.followStage], current);
    };
});
// 沟通一级结果过滤器
ppts.ng.filter('mainTask', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.mainTask], current);
    };
});
// 沟通二级结果过滤器
ppts.ng.filter('subTalk', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.subTalk], current);
    };
});
// 客户级别过滤器
ppts.ng.filter('customerLevel', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.customerLevel], current);
    };
});
// 无效原因过滤器
ppts.ng.filter('invalidReason', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.invalidReason], current);
    };
});
// 购买意图过滤器
ppts.ng.filter('purchaseIntension', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.purchaseIntension], current);
    };
});
// 实际上门人数过滤器
ppts.ng.filter('verifyPeople', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.verifyPeople], current);
    };
});
// 课时时长过滤器
ppts.ng.filter('period', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.period], current);
    };
});
// 接触方式过滤器
ppts.ng.filter('contactType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.contactType], current);
    };
});
// 产品大类过滤器
ppts.ng.filter('categoryType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.categoryType], current);
    };
});
// 服务费类型过滤器
ppts.ng.filter('expenseType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.expenseType], current);
    };
});
// 年级类型过滤器
ppts.ng.filter('gradeType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.gradeType], current);
    };
});
// 科目类型过滤器
ppts.ng.filter('subjectType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.subjectType], current);
    };
});
// 科目过滤器
ppts.ng.filter('subject', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.subject], current);
    };
});
// 季节过滤器
ppts.ng.filter('season', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.season], current);
    };
});
// 课次/课时时长过滤器
ppts.ng.filter('duration', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.duration], current);
    };
});
// 课程级别过滤器
ppts.ng.filter('courseLevel', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.courseLevel], current);
    };
});
// 辅导类型过滤器
ppts.ng.filter('coachType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.coachType], current);
    };
});
// 班组类型过滤器
ppts.ng.filter('groupType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.groupType], current);
    };
});
// 班级类型过滤器
ppts.ng.filter('classType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.classType], current);
    };
});
// 跨校区产品收入归属过滤器
ppts.ng.filter('belonging', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.belonging], current);
    };
});
// 薪酬规则对象过滤器
ppts.ng.filter('rule', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.rule], current);
    };
});
// 颗粒度过滤器
ppts.ng.filter('unit', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.unit], current);
    };
});
// 合作类型过滤器
ppts.ng.filter('hasPartner', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.hasPartner], current);
    };
});