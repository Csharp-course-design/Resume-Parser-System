go
create database ResumeBase;
go
use ResumeBase;
go
-- 简历实体
CREATE TABLE ResumeModel(
	Id INT PRIMARY KEY IDENTITY(1,1), --实体id
	FileName NVARCHAR(100), --文件名
	FileBase64 NVARCHAR(2000),--文件Nase64编码字符串
	ImportDate datetime --导入日期
);

----简历信息实体表
--CREATE TABLE ResumeImfo
--(
--    Id INT PRIMARY KEY IDENTITY(1,1),    -- 自动生成的唯一标识符
--	--ResumeId INT,
--    Name NVARCHAR(100),                   -- 姓名
--    Phone NVARCHAR(11),                   -- 电话
--    Age INT,                              -- 年龄

--    -- 教育背景
--    SchoolName NVARCHAR(200),             -- 学校名称
--    SchoolType NVARCHAR(50),              -- 学校类型 (例如：985, 211)
--    Degree NVARCHAR(50),                  -- 最高学位
--    Major NVARCHAR(100),                  -- 专业


--);

---- 工作经验实体表
--CREATE TABLE WorkExperience
--(
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    ResumeId INT,                         -- 外键关联 ResumeImfo 表
--    StartTimeYear NVARCHAR(4),            -- 开始时间年
--    StartTimeMonth NVARCHAR(2),           -- 开始时间月
--    EndTimeYear NVARCHAR(4),              -- 结束时间年
--    EndTimeMonth NVARCHAR(2),             -- 结束时间月
--    StillActive BIT,                      -- 是否仍在职
--    CompanyName NVARCHAR(200),            -- 公司名称
--    Department NVARCHAR(100),             -- 部门
--    Location NVARCHAR(100),               -- 工作地点
--    JobTitle NVARCHAR(100),               -- 职位名称

--);

---- 技能实体表：
--CREATE TABLE Skills
--(
--    Id INT PRIMARY KEY IDENTITY(1,1),
--    ResumeId INT,                         -- 外键关联 ResumeImfo 表
--    Skill NVARCHAR(100),                  -- 技能名称
--    CONSTRAINT FK_ResumeId_2 FOREIGN KEY (ResumeId) REFERENCES ResumeImfo(Id)
--);

--关键字实体
CREATE TABLE ResumKeyworldModel(
	Id INT PRIMARY KEY IDENTITY(1,1),--关键字id
	World NVARCHAR(50) --关键字
);


--简历-关键字关系表
CREATE TABLE RelationResumKeyworld(
	ResumeId INT, --简历id
	KeyId INT, --关键字id
	CONSTRAINT PK_ResumUser_IdWorld PRIMARY KEY (ResumeId, KeyId)
);

--简历-简历信息关系表
CREATE TABLE RelationResumInfor(
	ResumeModelId INT NOT NULL,
	ResumeImfoId INT NOT NULL,
	CONSTRAINT PK_ResumInfor PRIMARY KEY (ResumeModelId, ResumeImfoId)
);

----简历-简历技能关系表
--CREATE TABLE RelationResumSkill(
--	ResumeModelId INT NOT NULL,
--	SkillsId INT NOT NULL,
--	CONSTRAINT PK_ResumSkill PRIMARY KEY (ResumeModelId, SkillsId)
--);

----简历-简历工作经验关系表
--CREATE TABLE RelationResumWork(
--	ResumeModelId INT NOT NULL,
--	WorkId INT NOT NULL,
--	CONSTRAINT PK_ResumWorkRelation PRIMARY KEY (ResumeModelId, WorkId)
--);

--简历-简历关键字全连接视图
go
drop view ResumeKeyworldView ;

--GO
--CREATE VIEW ResumeKeyworldView AS
--SELECT 
--    rm.Id AS ResumeId,               -- 简历ID
--    COUNT(rkm.Id) AS KeyworldCount,   -- 关键字的频次（每个简历对应的关键字数量）
--    STRING_AGG(rkm.World, ', ') AS KeyworldName  -- 所有关键字名称的拼接（使用逗号分隔）
--FROM 
--    ResumeModel rm
--JOIN 
--    RelationResumKeyworld rkr ON rm.Id = rkr.ResumeId
--JOIN 
--    ResumKeyworldModel rkm ON rkr.KeyId = rkm.Id
--GROUP BY 
--    rm.Id;

GO
CREATE VIEW ResumeKeyworldView AS
SELECT 
    rm.Id AS ResumeId,               -- 简历ID
    rkm.World AS KeyworldName  -- 所有关键字名称
FROM 
    ResumeModel rm
JOIN 
    RelationResumKeyworld rkr ON rm.Id = rkr.ResumeId
JOIN 
    ResumKeyworldModel rkm ON rkr.KeyId = rkm.Id;




