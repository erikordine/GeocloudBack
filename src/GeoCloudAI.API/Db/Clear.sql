use geocloudai;

delete from drillbox where id > 13;
alter table drillbox AUTO_INCREMENT = 13;

delete from drillhole where id > 11;
alter table drillhole AUTO_INCREMENT = 11;

delete from minearea where id > 8;
alter table minearea AUTO_INCREMENT = 8;

delete from mineareastatus where id > 3;
alter table mineareastatus AUTO_INCREMENT = 3;

delete from mineareashape where id > 3;
alter table mineareashape AUTO_INCREMENT = 3;

delete from mineareatype where id > 3;
alter table mineareatype AUTO_INCREMENT = 3;

delete from mine where id > 6;
alter table mine AUTO_INCREMENT = 6;

delete from minesize where id > 3;
alter table minesize AUTO_INCREMENT = 3;

delete from minestatus where id > 3;
alter table minestatus AUTO_INCREMENT = 3;

delete from deposit where id > 4;
alter table deposit AUTO_INCREMENT = 4;

delete from metalgroupsub where id > 4;
alter table metalgroupsub AUTO_INCREMENT = 4;

delete from metalgroup where id > 5;
alter table metalgroup AUTO_INCREMENT = 5;

delete from oregenetictypesub where id > 6;
alter table oregenetictypesub AUTO_INCREMENT = 6;

delete from oregenetictype where id > 5;
alter table oregenetictype AUTO_INCREMENT = 5;

delete from depositType where id > 4;
alter table deposittype AUTO_INCREMENT = 4;

delete from region where id > 3;
alter table region AUTO_INCREMENT = 3;

delete from project where id > 4;
alter table project AUTO_INCREMENT = 4;

delete from projectType where id > 4;
alter table projecttype AUTO_INCREMENT = 4;

delete from projectStatus where id > 4;
alter table projectstatus AUTO_INCREMENT = 4;

delete from company where id > 4;
alter table company AUTO_INCREMENT = 4;

delete from companyType where id > 5;
alter table companyType AUTO_INCREMENT = 5;

delete from user where id > 3;
alter table user AUTO_INCREMENT = 3;

delete from profilerole where profileId > 4;
delete from profilerole where roleId > 5;

delete from role where id > 5;
alter table role AUTO_INCREMENT = 5;

delete from profile where id > 4;
alter table profile AUTO_INCREMENT = 4;

delete from account where id > 2;
alter table account AUTO_INCREMENT = 2;

delete from drillHoleType where id > 5;
alter table drillHoleType AUTO_INCREMENT = 5;

delete from drillingType where id > 8;
alter table drillingType AUTO_INCREMENT = 8;

delete from employee where id > 6;
alter table employee AUTO_INCREMENT = 6;

delete from employeeRole where id > 6;
alter table employeeRole AUTO_INCREMENT = 6;

delete from drillBoxStatus where id > 5;
alter table drillBoxStatus AUTO_INCREMENT = 5;

delete from drillBoxType where id > 5;
alter table drillBoxType AUTO_INCREMENT = 5;

delete from drillBoxMaterial where id > 5;
alter table drillBoxMaterial AUTO_INCREMENT = 5;

delete from coreShed where id > 5;
alter table coreShed AUTO_INCREMENT = 5;

delete from drillBoxActivityType where id > 5;
alter table drillBoxActivityType AUTO_INCREMENT = 5;

delete from unit where id > 10;
alter table unit AUTO_INCREMENT = 10;

delete from unitType where id > 6;
alter table unitType AUTO_INCREMENT = 6;

delete from functionalityType where id > 8;
alter table functionalityType AUTO_INCREMENT = 8;

commit;