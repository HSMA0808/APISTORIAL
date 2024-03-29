CREATE DATABASE APISTORIAL_v1
GO

USE APISTORIAL_v1
GO

CREATE TABLE NAMEVALUE
(IDNAMEVALUE INT PRIMARY KEY IDENTITY
,IDGROUP INT
,DESCRIPTION NVARCHAR(75)
,CUSTOMSTRING1 NVARCHAR(75)
,CUSTOMSTRING2 NVARCHAR(75)
,CUSTOMINT1 INT
,CUSTOMINT2 INT
,GROUP_NAME NVARCHAR(150)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE SPECIALTYS
(IDSPECIALTY INT PRIMARY KEY IDENTITY
,DESCRIPTION NVARCHAR(200)
,CODE NVARCHAR(10)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE ANALYSIS_TYPE
(IDANALYSIS_TYPE INT PRIMARY KEY IDENTITY
,DESCRIPTION NVARCHAR(200)
,CODE NVARCHAR(10)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE RESULT_TYPE
(IDRESULT_TYPE INT PRIMARY KEY IDENTITY
,DESCRIPTION NVARCHAR(200)
,CODE NVARCHAR(10)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE ANALYSIS
(IDANALYSIS INT PRIMARY KEY IDENTITY
,IDANALYSIS_TYPE INT CONSTRAINT FK_IDANALYSISTYPE_ANALYSIS_ANALYSISTYPE REFERENCES ANALYSIS_TYPE (IDANALYSIS_TYPE)
,IDRESULT_TYPE INT CONSTRAINT FK_IDRESULTTYPE_ANALYSIS_RESULTTYPE REFERENCES RESULT_TYPE (IDRESULT_TYPE)
,DESCRIPTION NVARCHAR(300)
,CODE NVARCHAR(10)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE OPERATION_TYPE
(IDOPERATION_TYPE INT PRIMARY KEY IDENTITY
,DESCRIPTION NVARCHAR(200)
,CODE NVARCHAR(10)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE OPERATION
(IDOPERATION INT PRIMARY KEY IDENTITY
,IDOPERATION_TYPE INT CONSTRAINT FK_IDOPERATIONTYPE_OPERATION_OPERATIONTYPE REFERENCES OPERATION_TYPE (IDOPERATION_TYPE)
,DESCRIPTION NVARCHAR(300)
,CODE NVARCHAR(10)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME
)
GO

CREATE TABLE MEDICAL_CENTER
(
IDMEDICAL_CENTER INT PRIMARY KEY IDENTITY
,DESCRIPTION NVARCHAR(300)
,RNC NVARCHAR(25)
,TEL1 NVARCHAR(15)
,TEL2 NVARCHAR(15)
,EMAIL1 NVARCHAR(200)
,EMAIL2 NVARCHAR(200)
,NAME_CONTACT NVARCHAR(500)
,NVSTATUS_CENTER INT CONSTRAINT FK_NVSTATUSCENTER_MEDICALCENTER_NAMEVALUE FOREIGN KEY (NVSTATUS_CENTER) REFERENCES NAMEVALUE (IDNAMEVALUE)
,TOKEN NVARCHAR(150)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE PATIENT
(IDPATIENT INT PRIMARY KEY IDENTITY
,FIRST_NAME NVARCHAR(75)
,MIDDLE_NAME NVARCHAR(75)
,LAST_NAME NVARCHAR(75)
,SEX NVARCHAR(1)
,NVIDENTIFICATION_TYPE INT CONSTRAINT FK_IDNAMEVALUE_PATIENT_NAMEVALUE FOREIGN KEY(NVIDENTIFICATION_TYPE) REFERENCES NAMEVALUE (IDNAMEVALUE)
,IDENTIFICATION_NUMBER VARCHAR(20)
,ADDRESS1 NVARCHAR(500)
,ADDRESS2 NVARCHAR(500)
,NVBLOOD_TYPE INT CONSTRAINT FK_NVBLOOD_PATIENT_NAMEVALUE FOREIGN KEY(NVBLOOD_TYPE) REFERENCES NAMEVALUE (IDNAMEVALUE)
,TEL1 NVARCHAR(15)
,TEL2 NVARCHAR(15)
,EMAIL NVARCHAR(200)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE DOCTOR
(IDDOCTOR INT PRIMARY KEY IDENTITY
,FIRST_NAME NVARCHAR(75)
,MIDDLE_NAME NVARCHAR(75)
,LAST_NAME NVARCHAR(75)
,SEX NVARCHAR(1)
,NVIDENTIFICATION_TYPE INT CONSTRAINT FK_IDNAMEVALUE_DOCTOR_NAMEVALUE FOREIGN KEY(NVIDENTIFICATION_TYPE) REFERENCES NAMEVALUE (IDNAMEVALUE)
,IDENTIFICATION_NUMBER VARCHAR(20)
,ADDRESS1 NVARCHAR(500)
,ADDRESS2 NVARCHAR(500)
,IDSPECIALTY INT CONSTRAINT FK_IDSPECIALTY_DOCTOR_SPECIALTYS FOREIGN KEY(IDSPECIALTY) REFERENCES SPECIALTYS (IDSPECIALTY)
,NVBLOOD_TYPE INT CONSTRAINT FK_NVBLOOD_DOCTOR_NAMEVALUE FOREIGN KEY(NVBLOOD_TYPE) REFERENCES NAMEVALUE (IDNAMEVALUE)
,TEL1 NVARCHAR(15)
,TEL2 NVARCHAR(15)
,EMAIL NVARCHAR(200)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE RECORD
(IDRECORD INT PRIMARY KEY IDENTITY
,IDPATIENT INT CONSTRAINT FK_IDPATIENT_RECORD_PATIENT FOREIGN KEY (IDPATIENT) REFERENCES PATIENT (IDPATIENT)
,MEDICALCENTER_CREATOR INT CONSTRAINT FK_LAST_MEDICALCENTERCREATOR_RECORD_MEDICALCENTER REFERENCES MEDICAL_CENTER (IDMEDICAL_CENTER)
,LAST_MEDICALCENTER_UPDATE INT CONSTRAINT FK_LAST_MEDICALCENTERUPDATE_RECORD_MEDICALCENTER REFERENCES MEDICAL_CENTER (IDMEDICAL_CENTER)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME)
GO

CREATE TABLE RECORD_VISITS
(
IDRECORD_VISITS INT PRIMARY KEY IDENTITY
,IDRECORD INT CONSTRAINT FK_LAST_IDRECORD_RECORDVISITS_RECORD REFERENCES RECORD (IDRECORD)
,IDDOCTOR INT CONSTRAINT FK_IDOCTOR_RECORDVISITS_DOCTOR REFERENCES DOCTOR (IDDOCTOR)
,IDSPECIALTY INT CONSTRAINT FK_IDSPECIALTY_RECORDVISITS_SPECIALTYS REFERENCES SPECIALTYS (IDSPECIALTY)
,OBSERVATIONS NVARCHAR(500)
,INDICATIONS NVARCHAR(500)
,VISIT_DATE DATETIME
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME
)
GO

CREATE TABLE RECORD_ANALYSIS
(
IDRECORD_ANALYSIS INT PRIMARY KEY IDENTITY
,IDRECORD INT CONSTRAINT FK_LAST_IDRECORD_RECORDANALYSIS_RECORD REFERENCES RECORD (IDRECORD)
,IDANALYSIS INT CONSTRAINT FK_IDANALYSIS_RECORDANALYSIS_ANALYSIS REFERENCES ANALYSIS (IDANALYSIS)
,PUBLIC_RESULTS BIT
,RESULTS NVARCHAR(500)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME
)
GO

CREATE TABLE RECORD_OPERATIONS
(
IDRECORD_OPERATIONS INT PRIMARY KEY IDENTITY
,IDRECORD INT CONSTRAINT FK_LAST_IDRECORD_RECORDOPERATIONS_RECORD REFERENCES RECORD (IDRECORD)
,IDOPERATION INT CONSTRAINT FK_IDOPERATION_RECORDOPERATION_OPERATION REFERENCES OPERATION (IDOPERATION)
,IDDOCTOR INT CONSTRAINT FK_IDDOCTOR_RECORDOPERATION_DOCTOR REFERENCES DOCTOR (IDDOCTOR)
,OPERATIONDATE DATETIME
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME
)
GO

CREATE TABLE RECORD_ALLERGIES
(
IDRECORD_ALLERGIES INT PRIMARY KEY IDENTITY
,IDRECORD INT CONSTRAINT FK_LAST_IDRECORD_RECORDALLERGIES_RECORD REFERENCES RECORD (IDRECORD)
,NVALLERGIE INT CONSTRAINT FK_NVALLERGIE_RECORDALLERGIES_NAMEVALUE REFERENCES NAMEVALUE (IDNAMEVALUE)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME
)
GO

CREATE TABLE RECORD_VACCINES
(
IDRECORD_VACCINES INT PRIMARY KEY IDENTITY
,IDRECORD INT CONSTRAINT FK_LAST_IDRECORD_RECORDVACCINES_RECORD REFERENCES RECORD (IDRECORD)
,NVVACCINE INT CONSTRAINT FK_NVVACCINE_RECORDVACCINE_NAMEVALUE REFERENCES NAMEVALUE (IDNAMEVALUE)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME
)
GO

CREATE TABLE RECORD_INTERMENTS
(
IDRECORD_INTERMENT INT PRIMARY KEY IDENTITY
,IDRECORD INT CONSTRAINT FK_LAST_IDRECORD_RECORDINTERMENTS_RECORD REFERENCES RECORD (IDRECORD)
,IDMEDICAL_CENTER INT CONSTRAINT FK_IDMEDICALCENTER_RECORDINTERMENTS_MEDICALCENTER REFERENCES MEDICAL_CENTER (IDMEDICAL_CENTER)
,INTERMENTDATE DATETIME
,REASON NVARCHAR(500)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME
)
GO

CREATE TABLE RECORD_EMERGENCYEMTRY
(
IDRECORD_EMERGENCYENTRY INT PRIMARY KEY IDENTITY
,IDRECORD INT CONSTRAINT FK_LAST_IDRECORD_RECORDEMERGENCYENTRY_RECORD REFERENCES RECORD (IDRECORD)
,IDMEDICAL_CENTER INT CONSTRAINT FK_IDMEDICALCENTER_RECORDEMERGENCYENTRY_MEDICALCENTER REFERENCES MEDICAL_CENTER (IDMEDICAL_CENTER)
,INTERMENTDATE DATETIME
,REASON NVARCHAR(500)
,CREATE_USER NVARCHAR(50)
,CREATE_DATE DATETIME
,UPDATE_USER NVARCHAR(50)
,UPDATE_DATE DATETIME
)
GO

-------PROCEDURES

CREATE PROCEDURE INSERT_DOCTOR
@FIRST_NAME NVARCHAR(75)
,@MIDDLE_NAME NVARCHAR(75)
,@LAST_NAME NVARCHAR(75)
,@SEX NVARCHAR(1)
,@NVIDENTIFICATION_TYPE INT 
,@IDENTIFICATION_NUMBER VARCHAR(20)
,@ADDRESS1 NVARCHAR(500)
,@ADDRESS2 NVARCHAR(500)
,@IDSPECIALTY INT 
,@NVBLOOD_TYPE INT
,@TEL1 NVARCHAR(15)
,@TEL2 NVARCHAR(15)
,@EMAIL NVARCHAR(200)
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME

AS
BEGIN
	INSERT INTO DOCTOR 
      (FIRST_NAME, MIDDLE_NAME, LAST_NAME, SEX, NVIDENTIFICATION_TYPE
    , IDENTIFICATION_NUMBER, ADDRESS1, ADDRESS2, IDSPECIALTY, NVBLOOD_TYPE, TEL1, TEL2, EMAIL
    , CREATE_USER, CREATE_DATE) 
    VALUES 
      (@FIRST_NAME, @MIDDLE_NAME, @LAST_NAME, @SEX, @NVIDENTIFICATION_TYPE
    , @IDENTIFICATION_NUMBER, @ADDRESS1, @ADDRESS2, @IDSPECIALTY, @NVBLOOD_TYPE, @TEL1, @TEL2, @EMAIL
    , @CREATE_USER, @CREATE_DATE)
END
GO


CREATE PROCEDURE INSERT_PATIENT
@FIRST_NAME NVARCHAR(75)
,@MIDDLE_NAME NVARCHAR(75)
,@LAST_NAME NVARCHAR(75)
,@SEX NVARCHAR(1)
,@NVIDENTIFICATION_TYPE INT
,@IDENTIFICATION_NUMBER VARCHAR(20)
,@ADDRESS1 NVARCHAR(500)
,@ADDRESS2 NVARCHAR(500)
,@NVBLOOD_TYPE INT
,@TEL1 NVARCHAR(15)
,@TEL2 NVARCHAR(15)
,@EMAIL NVARCHAR(200)
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME
AS
BEGIN
INSERT INTO PATIENT 
  (FIRST_NAME, MIDDLE_NAME, LAST_NAME, SEX, NVIDENTIFICATION_TYPE
, IDENTIFICATION_NUMBER, ADDRESS1, ADDRESS2, NVBLOOD_TYPE, TEL1, TEL2, EMAIL
, CREATE_USER, CREATE_DATE) 
VALUES 
  (@FIRST_NAME, @MIDDLE_NAME, @LAST_NAME, @SEX, @NVIDENTIFICATION_TYPE
, @IDENTIFICATION_NUMBER, @ADDRESS1, @ADDRESS2, @NVBLOOD_TYPE, @TEL1, @TEL2, @EMAIL
, @CREATE_USER, @CREATE_DATE)
END
GO

CREATE PROCEDURE INSERT_MEDICALCENTER
@DESCRIPTION NVARCHAR(300)
,@RNC NVARCHAR(25)
,@TEL1 NVARCHAR(15)
,@TEL2 NVARCHAR(15)
,@EMAIL1 NVARCHAR(200)
,@EMAIL2 NVARCHAR(200)
,@NAME_CONTACT NVARCHAR(500)
,@NVSTATUS_CENTER INT 
,@TOKEN NVARCHAR(150)
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME

AS
BEGIN
INSERT INTO MEDICAL_CENTER 
(DESCRIPTION, RNC, TEL1, TEL2, EMAIL1, EMAIL2
, NVSTATUS_CENTER, TOKEN, CREATE_USER, CREATE_DATE) 
VALUES 
(@DESCRIPTION, @RNC, @TEL1, @TEL2, @EMAIL1, @EMAIL2
, @NVSTATUS_CENTER, @TOKEN, @CREATE_USER, @CREATE_DATE)
END
GO

CREATE PROCEDURE INSERT_RECORD
@IDPATIENT INT
,@MEDICALCENTER_CREATOR INT
,@LAST_MEDICALCENTER_UPDATE INT
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME

AS
BEGIN
INSERT INTO RECORD 
(IDPATIENT, MEDICALCENTER_CREATOR, LAST_MEDICALCENTER_UPDATE, CREATE_DATE, CREATE_USER)
VALUES (@IDPATIENT, @MEDICALCENTER_CREATOR, @LAST_MEDICALCENTER_UPDATE, @CREATE_DATE, @CREATE_USER)
END
GO

CREATE PROCEDURE INSERT_RECORDANALYSIS
@IDRECORD INT 
,@IDANALYSIS INT
,@PUBLIC_RESULTS BIT
,@RESULTS NVARCHAR(500)
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME
AS
BEGIN
INSERT INTO RECORD_ANALYSIS
(IDRECORD, IDANALYSIS, PUBLIC_RESULTS, RESULTS, CREATE_USER, CREATE_DATE)
VALUES
(@IDRECORD, @IDANALYSIS, @PUBLIC_RESULTS, @RESULTS, @CREATE_USER, @CREATE_DATE)
END
GO

CREATE PROCEDURE INSERT_RECORDOPERATION
@IDRECORD INT
,@IDOPERATION INT 
,@IDDOCTOR INT 
,@OPERATIONDATE DATETIME
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME
AS
BEGIN
INSERT INTO RECORD_OPERATIONS 
    (IDRECORD,
     IDOPERATION,
     IDDOCTOR,
     OPERATIONDATE,
     CREATE_USER,
     CREATE_DATE)
VALUES
    (@IDRECORD,
     @IDOPERATION,
     @IDDOCTOR,
     @OPERATIONDATE,
     @CREATE_USER,
     @CREATE_DATE)
END
GO

CREATE PROCEDURE INSERT_RECORDVISIT
@IDRECORD INT 
,@IDDOCTOR INT  
,@IDSPECIALTY INT  
,@OBSERVATIONS NVARCHAR(500)
,@INDICATIONS NVARCHAR(500)
,@VISIT_DATE DATETIME
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME
AS
BEGIN
	INSERT INTO RECORD_VISITS
    (IDRECORD, IDDOCTOR, IDSPECIALTY, OBSERVATIONS, INDICATIONS, VISIT_DATE
    , CREATE_USER, CREATE_DATE)
    VALUES
    (@IDRECORD, @IDDOCTOR, @IDSPECIALTY, @OBSERVATIONS, @INDICATIONS, @VISIT_DATE
    , @CREATE_USER, @CREATE_DATE)
END
GO

CREATE PROCEDURE INSERT_RECORDALLERGIES
@IDRECORD INT 
,@NVALLERGIE INT 
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME
AS
BEGIN
INSERT INTO RECORD_ALLERGIES(
IDRECORD,
NVALLERGIE,
CREATE_USER,
CREATE_DATE)
VALUES
(@IDRECORD,
@NVALLERGIE,
@CREATE_USER,
@CREATE_DATE)
END
GO

CREATE PROCEDURE INSERT_RECORDEMERGENCYENTRY
@IDRECORD INT
,@IDMEDICAL_CENTER INT
,@INTERMENTDATE DATETIME
,@REASON NVARCHAR(500)
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME
AS
BEGIN
INSERT INTO RECORD_EMERGENCYEMTRY
(IDRECORD,
 IDMEDICAL_CENTER,
 INTERMENTDATE,
 REASON,
 CREATE_USER,
 CREATE_DATE)
 VALUES
(@IDRECORD,
 @IDMEDICAL_CENTER,
 @INTERMENTDATE,
 @REASON,
 @CREATE_USER,
 @CREATE_DATE)
END
GO

CREATE PROCEDURE INSERT_RECORDINTERMENTS
@IDRECORD INT 
,@IDMEDICAL_CENTER INT
,@INTERMENTDATE DATETIME
,@REASON NVARCHAR(500)
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME
AS
BEGIN
INSERT INTO RECORD_INTERMENTS
(IDRECORD,
 IDMEDICAL_CENTER,
 INTERMENTDATE,
 REASON,
 CREATE_USER,
 CREATE_DATE)
 VALUES
(@IDRECORD,
 @IDMEDICAL_CENTER,
 @INTERMENTDATE,
 @REASON,
 @CREATE_USER,
 @CREATE_DATE)
END
GO

CREATE PROCEDURE INSERT_RECORDVACCINES
@IDRECORD INT 
,@NVVACCINE INT
,@CREATE_USER NVARCHAR(50)
,@CREATE_DATE DATETIME
--,@UPDATE_USER NVARCHAR(50)
--,@UPDATE_DATE DATETIME
AS
BEGIN
INSERT INTO RECORD_VACCINES
(IDRECORD,
 NVVACCINE,
 CREATE_USER,
 CREATE_DATE)
 VALUES
 (@IDRECORD,
 @NVVACCINE,
 @CREATE_USER,
 @CREATE_DATE
 )
END
GO

----DATA STANDARD

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (1, 'MASCULINO', 'M', 0, 'SEX_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (1, 'FEMENINO', 'F', 0, 'SEX_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (1, 'OTRO', 'O', 0, 'SEX_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (2, 'CEDULA', 'C', 0, 'IDENTIFICATIONTYPE_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (2, 'PASAPORTE', 'P', 0, 'IDENTIFICATIONTYPE_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (3, 'ACTIVE', 'A', 0, 'STATUS_MEDICALCENTER_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (3, 'PENDING', 'P', 0, 'STATUS_MEDICALCENTER_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (4, 'B+', 'B+', 0, 'BLOODTYPE_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (4, 'A+', 'A+', 0, 'BLOODTYPE_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (4, 'B-', 'B-', 0, 'BLOODTYPE_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (4, 'A-', 'A-', 0, 'BLOODTYPE_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (4, 'O-', 'O-', 0, 'BLOODTYPE_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (4, 'O+', 'O+', 0, 'BLOODTYPE_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (5, 'POLVO', 'POLVO', 0, 'ALLERGIES_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (5, 'POLEM', 'POLEM', 0, 'ALLERGIES_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (5, 'ABEJAS', 'ABEJAS', 0, 'ALLERGIES_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (6, 'COVID', 'COVID', 0, 'VACCINES_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (6, 'PAPILOMA', 'PAPILOMA', 0, 'VACCINES_GROUP', 'SERVER', GETDATE())

INSERT INTO NAMEVALUE (IDGROUP, DESCRIPTION, CUSTOMSTRING1, CUSTOMINT1, GROUP_NAME, CREATE_USER, CREATE_DATE)
VALUES (6, 'GRIPE AVIAR', 'GRIPE_AVIAR', 0, 'VACCINES_GROUP', 'SERVER', GETDATE())


INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('MEDICINA GENERAL', '00', 'SERVER', GETDATE())

INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('OFTALMOLOGIA', '02', 'SERVER', GETDATE())

INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('ODONTOLOGIA', '03', 'SERVER', GETDATE())

INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('ONCOLOGIA', '04', 'SERVER', GETDATE())

INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('NEUROLOGIA', '05', 'SERVER', GETDATE())

INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('UROLOGIA', '06', 'SERVER', GETDATE())

INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('GINECOLOGIA', '07', 'SERVER', GETDATE())

INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('PEDIATRIA', '08', 'SERVER', GETDATE())

INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('PSICOLOGIA', '09', 'SERVER', GETDATE())

INSERT INTO SPECIALTYS (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES ('PSIQUITRIA', '10', 'SERVER', GETDATE())

INSERT INTO ANALYSIS_TYPE (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES('LABORATORIO', '00', 'SERVER', GETDATE())

INSERT INTO ANALYSIS_TYPE (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES('IMAGENES', '01', 'SERVER', GETDATE())

INSERT INTO RESULT_TYPE (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES('BINARIO', '00', 'SERVER', GETDATE())

INSERT INTO RESULT_TYPE (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES('DESCRIPCION', '01', 'SERVER', GETDATE())

INSERT INTO OPERATION_TYPE(DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES('MEDICA', '00', 'SERVER', GETDATE())

INSERT INTO OPERATION_TYPE (DESCRIPTION, CODE, CREATE_USER, CREATE_DATE)
VALUES('PLASTICA', '01', 'SERVER', GETDATE())




