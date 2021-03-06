DECLARE @SQL NVARCHAR(255);

WHILE EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS	WHERE CONSTRAINT_TYPE = 'FOREIGN KEY')
BEGIN
    SELECT   @SQL = 'ALTER TABLE ' + TABLE_NAME + ' DROP CONSTRAINT ' + CONSTRAINT_NAME 
    FROM    INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
	WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'
    EXEC    SP_EXECUTESQL @SQL
END;

WHILE EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE')
BEGIN
    SELECT    @SQL = 'DROP TABLE ' + TABLE_NAME
    FROM    INFORMATION_SCHEMA.TABLES
    EXEC    SP_EXECUTESQL @SQL
END
GO

CREATE TABLE CLINIC (
    ID        			BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE		DATETIME2 NOT NULL,
    NAME      			VARCHAR(255),
    ADDRESS   			VARCHAR(255)
)

ALTER TABLE CLINIC ADD CONSTRAINT CLINIC_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE DIAGNOSTICS (
    ID                 BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE	   DATETIME2 NOT NULL,
    EXAMINATION_DATE   DATETIME2 NOT NULL,
    PATIENT_VISIT_ID   BIGINT NOT NULL,
    EXAMINATION_ID     BIGINT NOT NULL,
    UNIT_PLAN_ID       BIGINT NOT NULL,
	EMPLOYEE_ID		   BIGINT NOT NULL
)

ALTER TABLE DIAGNOSTICS ADD CONSTRAINT DIAGNOSTICS_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE DISEASE (
    ID                 BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE	   DATETIME2 NOT NULL,
    CODE               VARCHAR(10) NOT NULL,
    CODE_DESCRIPTION   VARCHAR(255) NOT NULL
)

ALTER TABLE DISEASE ADD CONSTRAINT DISEASE_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE EMPLACEMENT (
    ID                 BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE	   DATETIME2 NOT NULL,
    EMPLACEMENT_NAME   VARCHAR(255) NOT NULL
)

ALTER TABLE EMPLACEMENT ADD CONSTRAINT EMPLACEMENT_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE EMPLOYEE (
    ID               BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE	 DATETIME2 NOT NULL,
    SALARY           MONEY NOT NULL,
    HIRE_DATE        DATETIME2 NOT NULL,
    EMPLOYEE_ID      BIGINT,
    PERSON_ID        BIGINT NOT NULL,
    UNIT_ID          BIGINT NOT NULL,
    EMPLACEMENT_ID   BIGINT NOT NULL
)

ALTER TABLE EMPLOYEE ADD CONSTRAINT EMPLOYEE_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE EMPLOYEE_COST (
    ID                 BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE	   DATETIME2 NOT NULL,
    EMPLOYEE_COST      MONEY NOT NULL,
    REALIZATION_DATE   DATETIME2 NOT NULL,
    EMPLOYEE_ID        BIGINT NOT NULL,
    UNIT_PLAN_ID       BIGINT NOT NULL
)

ALTER TABLE EMPLOYEE_COST ADD CONSTRAINT EMPLOYEE_COST_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE EXAMINATION (
    ID                 BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE	   DATETIME2 NOT NULL,
    EXAMINATION_NAME   VARCHAR(255) NOT NULL,
    COST               MONEY NOT NULL
)

ALTER TABLE EXAMINATION ADD CONSTRAINT EXAMINATION_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE MEDICINE_ORDER 
    (
    ID                  	BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE	  	    DATETIME2 NOT NULL,
    COST                	MONEY NOT NULL,
    EXPIRE_DATE          	DATETIME2 NOT NULL,
    MEDICINE_BATCH_SERIES	VARCHAR(255) NOT NULL,
    MEDICINE_TYPE_ID   		BIGINT NOT NULL
) 

ALTER TABLE MEDICINE_ORDER ADD CONSTRAINT MEDICINE_ORDER_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE MEDICINE_TYPE 
    (
    ID                  BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE		DATETIME2 NOT NULL,
    MEDICINE_NAME       VARCHAR(255) NOT NULL,
    ACTIVE_INGREDIENT   VARCHAR(255) NOT NULL
)
ALTER TABLE MEDICINE_TYPE ADD CONSTRAINT MEDICINE_TYPE_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE PATIENT_DIAGNOSE (
    ID                 BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE	   DATETIME2 NOT NULL,
    DIAGNOSE           VARCHAR(255) NOT NULL,
    PATIENT_VISIT_ID   BIGINT NOT NULL,
    DISEASE_ID         BIGINT NOT NULL
)

ALTER TABLE PATIENT_DIAGNOSE ADD CONSTRAINT PATIENT_DIAGNOSE_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE PATIENT_MEDICINES (
    ID						BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE			DATETIME2 NOT NULL,
    DOSE					VARCHAR(50) NOT NULL,
    TREATMENT_DATE			DATETIME2 NOT NULL,
    PATIENT_VISIT_ID		BIGINT NOT NULL,
	MEDICINE_ORDER_ID		BIGINT NOT NULL
)

ALTER TABLE PATIENT_MEDICINES ADD CONSTRAINT PATIENT_MEDICINES_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE PATIENT_VISIT (
    ID                 BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE	   DATETIME2 NOT NULL,
    DATE_FROM          DATETIME2 NOT NULL,
    DATE_TO            DATETIME2,
    PERSON_ID          BIGINT NOT NULL,
    CLINIC_ID          BIGINT NOT NULL,
    UNIT_ID            BIGINT NOT NULL
)

ALTER TABLE PATIENT_VISIT ADD CONSTRAINT PATIENT_VISIT_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE PERSON (
    ID          		BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE		DATETIME2 NOT NULL,
    NAME         		VARCHAR(255) NOT NULL,
    LAST_NAME 		   	VARCHAR(255) NOT NULL,
    ADDRESS    		  	VARCHAR(255) NOT NULL,
    PESEL      			VARCHAR(11) NOT NULL,
    BIRTH_DATE   		DATE
)

ALTER TABLE PERSON ADD CONSTRAINT PERSON_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE UNIT (
    ID             		BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE		DATETIME2 NOT NULL,
    CLINIC_ID			BIGINT NOT NULL,
    UNIT_TYPE_ID		BIGINT NOT NULL,
    UNIT_ID				BIGINT
)

ALTER TABLE UNIT ADD CONSTRAINT UNIT_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE UNIT_PLAN (
    ID					BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE		DATETIME2 NOT NULL,
    BUDGET_TYPE			VARCHAR(255) NOT NULL,
    DATE_FROM			DATETIME2 NOT NULL,
    DATE_TO				DATETIME2 NOT NULL,
    VALUE				MONEY NOT NULL,
    UNIT_ID				BIGINT NOT NULL
)

ALTER TABLE UNIT_PLAN ADD CONSTRAINT UNIT_PLAN_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

CREATE TABLE UNIT_TYPE (
    ID      		    BIGINT IDENTITY(1,1) NOT NULL,
	LAST_MOD_DATE		DATETIME2 NOT NULL,
    UNIT_NAME			VARCHAR(255) NOT NULL
)

ALTER TABLE UNIT_TYPE ADD CONSTRAINT UNIT_TYPE_PK PRIMARY KEY CLUSTERED (ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) 

ALTER TABLE DIAGNOSTICS
    ADD CONSTRAINT DIAGNOSTICS_EXAMINATION_FK FOREIGN KEY ( EXAMINATION_ID )
        REFERENCES EXAMINATION ( ID )

ALTER TABLE DIAGNOSTICS
    ADD CONSTRAINT DIAGNOSTICS_PATIENT_VISIT_FK FOREIGN KEY ( PATIENT_VISIT_ID )
        REFERENCES PATIENT_VISIT ( ID )

ALTER TABLE DIAGNOSTICS
    ADD CONSTRAINT DIAGNOSTICS_UNIT_PLAN_FK FOREIGN KEY ( UNIT_PLAN_ID )
        REFERENCES UNIT_PLAN ( ID )

ALTER TABLE DIAGNOSTICS
    ADD CONSTRAINT DIAGNOSTICS_EMPLOYEE_FK FOREIGN KEY ( EMPLOYEE_ID )
        REFERENCES EMPLOYEE ( ID )

ALTER TABLE EMPLOYEE_COST
    ADD CONSTRAINT EMPLOYEE_COST_EMPLOYEE_FK FOREIGN KEY ( EMPLOYEE_ID )
        REFERENCES EMPLOYEE ( ID )

ALTER TABLE EMPLOYEE_COST
    ADD CONSTRAINT EMPLOYEE_COST_UNIT_PLAN_FK FOREIGN KEY ( UNIT_PLAN_ID )
        REFERENCES UNIT_PLAN ( ID )

ALTER TABLE EMPLOYEE
    ADD CONSTRAINT EMPLOYEE_EMPLACEMENT_FK FOREIGN KEY ( EMPLACEMENT_ID )
        REFERENCES EMPLACEMENT ( ID )

ALTER TABLE EMPLOYEE
    ADD CONSTRAINT EMPLOYEE_EMPLOYEE_FK FOREIGN KEY ( EMPLOYEE_ID )
        REFERENCES EMPLOYEE ( ID )

ALTER TABLE EMPLOYEE
    ADD CONSTRAINT EMPLOYEE_PERSON_FK FOREIGN KEY ( PERSON_ID )
        REFERENCES PERSON ( ID )

ALTER TABLE EMPLOYEE
    ADD CONSTRAINT EMPLOYEE_UNIT_FK FOREIGN KEY ( UNIT_ID )
        REFERENCES UNIT ( ID )

ALTER TABLE MEDICINE_ORDER
    ADD CONSTRAINT MEDICINE_ORDER_MEDICINE_TYPE_FK FOREIGN KEY ( MEDICINE_TYPE_ID )
        REFERENCES MEDICINE_TYPE ( ID )

ALTER TABLE PATIENT_DIAGNOSE
    ADD CONSTRAINT PATIENT_DIAGNOSE_DISEASE_FK FOREIGN KEY ( DISEASE_ID )
        REFERENCES DISEASE ( ID )

ALTER TABLE PATIENT_DIAGNOSE
    ADD CONSTRAINT PATIENT_DIAGNOSE_PATIENT_VISIT_FK FOREIGN KEY ( PATIENT_VISIT_ID )
        REFERENCES PATIENT_VISIT ( ID )

ALTER TABLE PATIENT_MEDICINES
    ADD CONSTRAINT PATIENT_MEDICINES_MEDICINE_ORDER_FK FOREIGN KEY ( MEDICINE_ORDER_ID )
        REFERENCES MEDICINE_ORDER ( ID )

ALTER TABLE PATIENT_MEDICINES
    ADD CONSTRAINT PATIENT_MEDICINES_PATIENT_VISIT_FK FOREIGN KEY ( PATIENT_VISIT_ID )
        REFERENCES PATIENT_VISIT ( ID )

ALTER TABLE PATIENT_VISIT
    ADD CONSTRAINT PATIENT_VISIT_CLINIC_FK FOREIGN KEY ( CLINIC_ID )
        REFERENCES CLINIC ( ID )

ALTER TABLE PATIENT_VISIT
    ADD CONSTRAINT PATIENT_VISIT_PERSON_FK FOREIGN KEY ( PERSON_ID )
        REFERENCES PERSON ( ID )

ALTER TABLE PATIENT_VISIT
    ADD CONSTRAINT PATIENT_VISIT_UNIT_FK FOREIGN KEY ( UNIT_ID )
        REFERENCES UNIT ( ID )

ALTER TABLE UNIT
    ADD CONSTRAINT UNIT_CLINIC_FK FOREIGN KEY ( CLINIC_ID )
        REFERENCES CLINIC ( ID )

ALTER TABLE UNIT_PLAN
    ADD CONSTRAINT UNIT_PLAN_UNIT_FK FOREIGN KEY ( UNIT_ID )
        REFERENCES UNIT ( ID )

ALTER TABLE UNIT
    ADD CONSTRAINT UNIT_UNIT_FK FOREIGN KEY ( UNIT_ID )
        REFERENCES UNIT ( ID )

ALTER TABLE UNIT
    ADD CONSTRAINT UNIT_UNIT_TYPE_FK FOREIGN KEY ( UNIT_TYPE_ID )
        REFERENCES UNIT_TYPE ( ID )

CREATE TABLE [DBO].[ASPNETROLES](
	[ID] [NVARCHAR](128) NOT NULL,
	[NAME] [NVARCHAR](256) NOT NULL,
 CONSTRAINT [PK_DBO.ASPNETROLES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [DBO].[ASPNETUSERCLAIMS](
	[ID] [INT] IDENTITY(1,1) NOT NULL,
	[USERID] [NVARCHAR](128) NOT NULL,
	[CLAIMTYPE] [NVARCHAR](MAX) NULL,
	[CLAIMVALUE] [NVARCHAR](MAX) NULL,
 CONSTRAINT [PK_DBO.ASPNETUSERCLAIMS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [DBO].[ASPNETUSERLOGINS](
	[LOGINPROVIDER] [NVARCHAR](128) NOT NULL,
	[PROVIDERKEY] [NVARCHAR](128) NOT NULL,
	[USERID] [NVARCHAR](128) NOT NULL,
 CONSTRAINT [PK_DBO.ASPNETUSERLOGINS] PRIMARY KEY CLUSTERED 
(
	[LOGINPROVIDER] ASC,
	[PROVIDERKEY] ASC,
	[USERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [DBO].[ASPNETUSERROLES](
	[USERID] [NVARCHAR](128) NOT NULL,
	[ROLEID] [NVARCHAR](128) NOT NULL,
 CONSTRAINT [PK_DBO.ASPNETUSERROLES] PRIMARY KEY CLUSTERED 
(
	[USERID] ASC,
	[ROLEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [DBO].[ASPNETUSERS](
	[ID] [NVARCHAR](128) NOT NULL,
	[EMAIL] [NVARCHAR](256) NULL,
	[EMAILCONFIRMED] [BIT] NOT NULL,
	[PASSWORDHASH] [NVARCHAR](MAX) NULL,
	[SECURITYSTAMP] [NVARCHAR](MAX) NULL,
	[PHONENUMBER] [NVARCHAR](MAX) NULL,
	[PHONENUMBERCONFIRMED] [BIT] NOT NULL,
	[TWOFACTORENABLED] [BIT] NOT NULL,
	[LOCKOUTENDDATEUTC] [DATETIME] NULL,
	[LOCKOUTENABLED] [BIT] NOT NULL,
	[ACCESSFAILEDCOUNT] [INT] NOT NULL,
	[USERNAME] [NVARCHAR](256) NOT NULL,
 CONSTRAINT [PK_DBO.ASPNETUSERS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [DBO].[ASPNETUSERCLAIMS]  WITH CHECK ADD  CONSTRAINT [FK_DBO.ASPNETUSERCLAIMS_DBO.ASPNETUSERS_USERID] FOREIGN KEY([USERID])
REFERENCES [DBO].[ASPNETUSERS] ([ID])
ON DELETE CASCADE

ALTER TABLE [DBO].[ASPNETUSERCLAIMS] CHECK CONSTRAINT [FK_DBO.ASPNETUSERCLAIMS_DBO.ASPNETUSERS_USERID]

ALTER TABLE [DBO].[ASPNETUSERLOGINS]  WITH CHECK ADD  CONSTRAINT [FK_DBO.ASPNETUSERLOGINS_DBO.ASPNETUSERS_USERID] FOREIGN KEY([USERID])
REFERENCES [DBO].[ASPNETUSERS] ([ID])
ON DELETE CASCADE

ALTER TABLE [DBO].[ASPNETUSERLOGINS] CHECK CONSTRAINT [FK_DBO.ASPNETUSERLOGINS_DBO.ASPNETUSERS_USERID]

ALTER TABLE [DBO].[ASPNETUSERROLES]  WITH CHECK ADD  CONSTRAINT [FK_DBO.ASPNETUSERROLES_DBO.ASPNETROLES_ROLEID] FOREIGN KEY([ROLEID])
REFERENCES [DBO].[ASPNETROLES] ([ID])
ON DELETE CASCADE

ALTER TABLE [DBO].[ASPNETUSERROLES] CHECK CONSTRAINT [FK_DBO.ASPNETUSERROLES_DBO.ASPNETROLES_ROLEID]

ALTER TABLE [DBO].[ASPNETUSERROLES]  WITH CHECK ADD  CONSTRAINT [FK_DBO.ASPNETUSERROLES_DBO.ASPNETUSERS_USERID] FOREIGN KEY([USERID])
REFERENCES [DBO].[ASPNETUSERS] ([ID])
ON DELETE CASCADE

ALTER TABLE [DBO].[ASPNETUSERROLES] CHECK CONSTRAINT [FK_DBO.ASPNETUSERROLES_DBO.ASPNETUSERS_USERID]

ALTER TABLE DBO.PERSON
ADD USER_ID NVARCHAR(128) NOT NULL;

ALTER TABLE DBO.PERSON
ADD FOREIGN KEY (USER_ID) 
REFERENCES DBO.ASPNETUSERS(ID)

--ADMINISTRATOR ACCOUNT INSERT
INSERT [dbo].[ASPNETUSERS] ([ID], [EMAIL], [EMAILCONFIRMED], [PASSWORDHASH], [SECURITYSTAMP], [PHONENUMBER], [PHONENUMBERCONFIRMED], [TWOFACTORENABLED], [LOCKOUTENDDATEUTC], [LOCKOUTENABLED], [ACCESSFAILEDCOUNT], [USERNAME]) 
VALUES (N'1A5AB777-1F4D-41AB-A9D2-DA10DC4FD19F', N'admin1@luxcover.pl', 0, N'AIc1dj7hnUhcU/Mp3VlMgmvUyeaFeX/xQMQ+p23ItgpKaGehDDiEsJ9Ken2wenk1TA==', N'b2249122-3ca8-4379-bf16-e4ab953cc19e', NULL, 0, 0, NULL, 1, 0, N'admin1@luxcover.pl')
INSERT [dbo].[ASPNETROLES] ([ID], [NAME]) VALUES (N'144704F5-F084-43CB-8392-ABF2509BEEDE', N'RECEPTIONIST')
INSERT [dbo].[ASPNETROLES] ([ID], [NAME]) VALUES (N'1A3C7046-B49D-4F01-85B6-5C5C348BDE58', N'ADMINISTRATOR')
INSERT [dbo].[ASPNETROLES] ([ID], [NAME]) VALUES (N'3CB9461C-430F-4625-A17D-AAFA9A58C081', N'DOCTOR')
INSERT [dbo].[ASPNETROLES] ([ID], [NAME]) VALUES (N'70178FE0-5539-4DF8-9989-AD89C77C50B1', N'PATIENT')
INSERT [dbo].[ASPNETROLES] ([ID], [NAME]) VALUES (N'FF5D4C30-25B3-423D-ADF3-405E7847F51F', N'MANAGER')
INSERT [dbo].[ASPNETUSERROLES] ([USERID], [ROLEID]) VALUES (N'1A5AB777-1F4D-41AB-A9D2-DA10DC4FD19F', N'1A3C7046-B49D-4F01-85B6-5C5C348BDE58')


SET IDENTITY_INSERT [dbo].[PERSON] ON

INSERT [dbo].[PERSON] ([ID], [LAST_MOD_DATE], [NAME], [LAST_NAME], [ADDRESS], [PESEL], [BIRTH_DATE], [USER_ID]) VALUES (1, GETDATE(), N'Administrator', N'Administrator', N'Administrator', N'00000000000', GETDATE(), N'1A5AB777-1F4D-41AB-A9D2-DA10DC4FD19F')

SET IDENTITY_INSERT [dbo].[PERSON] OFF
SET IDENTITY_INSERT [dbo].[EMPLACEMENT] ON

INSERT [dbo].[EMPLACEMENT] ([ID], [LAST_MOD_DATE], [EMPLACEMENT_NAME]) VALUES (1, GETDATE(), N'Administrator')
INSERT [dbo].[EMPLACEMENT] ([ID], [LAST_MOD_DATE], [EMPLACEMENT_NAME]) VALUES (2, GETDATE(), N'Manager')

SET IDENTITY_INSERT [dbo].[EMPLACEMENT] OFF