create or replace function AnnualReport(y int)
returns table(id int, name text, month int, hours dec)
as 'select p."Id", p."Name", extract(month from c."Date")::integer, sum(t."Hours")::decimal
from "Calendar" as c join "Tasks" as t on t."DayId"=c."Id" join "Projects" as p on t."ProjectId" =p."Id"
where extract(year from c."Date")=y and c."Deleted"=false and p."Deleted"=false and t."Deleted"=false
group by p."Id", p."Name", extract(month from c."Date")
order by p."Id"'
language sql;


create or replace function MonthlyReport(y int, m int)
returns table(empId int, empName text, projId int, projName text, hours decimal)
as 'select e."Id", e."FirstName" || '' '' || e."LastName", p."Id", p."Name", sum(t."Hours")::decimal
from "Employees" as e join "Calendar" as c on e."Id"=c."EmployeeId" join "Tasks" as t on c."Id"=t."DayId" join "Projects" as p on p."Id"=t."ProjectId"
where extract(year from c."Date")=y and extract(month from c."Date")=m
		and e."Deleted"=false and c."Deleted"=false and t."Deleted"=false and p."Deleted"=false
group by e."Id", e."FirstName", e."LastName", p."Id", p."Name"
order by e."Id"'
language sql;

create or replace function ProjectHistory(y int)
returns table(empId int, empName text, hours decimal, year integer)
as 'select e."Id", e."FirstName" || '' '' || e."LastName", sum(t."Hours")::decimal, extract(year from c."Date")::integer 
from "Employees" as e join "Calendar" as c on e."Id"=c."EmployeeId" join "Tasks" as t on c."Id"=t."DayId" join "Projects" as p on p."Id"=t."ProjectId"
where p."Id"=y and e."Deleted"=false and c."Deleted"=false and t."Deleted"=false and p."Deleted"=false
group by e."Id", e."FirstName", e."LastName", extract(year from c."Date")
order by e."Id"'
language sql;

create or replace function CompanyWorkingHoursData(y int, m int)
returns table(employeeId int, teamId int, teamName text, roleId int, roleName text, 
				roleHourlyPrice dec, roleMonthlyPrice dec, 
				projectId int, projectName text, projectAmount dec, 
				projectPricingId int, projectPricingName text, workingHours dec)
as 'select 	res."EmployeeId", res."TeamId", res."TeamName",
		res."RoleId", res."RoleName", res."RoleHourlyPrice" , res."RoleMonthlyPrice",
		res."ProjectId", res."ProjectName", res."ProjectAmount",
		res."ProjectPricingId",
		res."ProjectPricingName",
		sum(res."Hours") as "WorkingHours" from
(select distinct on (t."Id") 
		r."Id" as "RoleId", r."Name" as "RoleName", r."HourlyPrice" as "RoleHourlyPrice", r."MonthlyPrice" as "RoleMonthlyPrice",
		e."Id" as "EmployeeId", te."Id" as "TeamId", te."Name" as "TeamName",
		p."Id" as "ProjectId", p."Name" as "ProjectName", p."Amount" as "ProjectAmount",
		p."PricingId" as "ProjectPricingId", ps."Name" as "ProjectPricingName",
		t."Hours" as "Hours"
from	
		"Roles" as r join "Members" as me on r."Id" = me."RoleId" 
		join "Employees" as e on e."Id" = me."EmployeeId"
		join "Calendar" as c on e."Id" = c."EmployeeId"
		join "Tasks" as t on c."Id" = t."DayId"
		join "Projects" as p on t."ProjectId" = p."Id"
		join "PricingStatuses" as ps on ps."Id" = p."PricingId"
		join "Teams" as te on te."Id" = p."TeamId"
where	extract(year from c."Date") = y
		and extract(month from c."Date") = m) as res
group by 	res."RoleId", res."RoleName", res."RoleHourlyPrice", res."RoleMonthlyPrice", 
			res."EmployeeId", res."TeamId", res."TeamName",
			res."ProjectId", res."ProjectName", res."ProjectAmount",
			res."ProjectPricingId", res."ProjectPricingName"
order by res."EmployeeId"'
language sql;

create or replace function CompanyOvertimeHours(y int, m int)
returns table(employeeId int, employeeFirstName text, employeeLastName text, overtimeHours dec)
as 'select  res."EmployeeId", res."FirstName", res."LastName", sum("Overtime") as "Overtime" from
(select e."Id" as "EmployeeId", e."FirstName", e."LastName", c."Id" as "DayId", c."Date", coalesce(sum(t."Hours"), 0) as "WorkingHours",
case 	when (c."DayTypeId" = 1) and (extract(isodow from c."Date") >= 6) then (coalesce(sum(t."Hours"), 0))
		when (c."DayTypeId" = 1) and (extract(isodow from c."Date") < 6) and (coalesce(sum(t."Hours"), 0) > 8) then (coalesce(sum(t."Hours"), 0) - 8)
		else 0 end
		as "Overtime"
from	"Employees" as e join "Calendar" as c on e."Id" = c."EmployeeId"
		join "Tasks" as t on c."Id" = t."DayId"
where	extract(year from c."Date") = y
		and extract(month from c."Date") = m		
group by e."Id", c."Id") as res
group by res."EmployeeId", res."FirstName", res."LastName"
order by res."EmployeeId"'
language sql;

create or replace function EmployeeHoursByDayType(y int, m int)
returns table(teamId int, employeeId int, employeeFirstName text, employeeLastName text, 
		dayTypeId int, dayTypeName text, dayHours dec)
as 'select 	res."TeamId", res."EmployeeId", res."EmployeeFirstName", res."EmployeeLastName", 
		res."DayTypeId", res."DayTypeName", coalesce(sum(res."DayHours"), 0) as "DayHours"
from
(select  coalesce(p."TeamId", 0) as "TeamId", 
		e."Id" as "EmployeeId", e."FirstName" as "EmployeeFirstName", e."LastName" as "EmployeeLastName",
		dt."Id" as "DayTypeId", dt."Name" as "DayTypeName",
		c."Id" as "DayId",
		ta."Id" as "TaskId",
case 	when dt."Id" = 1 then ta."Hours"
		when dt."Id" <> 1 then 8
		else 0 end
		as "DayHours"
from 	"Employees" as e
		join "Calendar" as c on e."Id" = c."EmployeeId"
		join "DayTypes" as dt on c."DayTypeId" = dt."Id"
		left join "Tasks" as ta on c."Id" = ta."DayId"
		left join "Projects" as p on p."Id" = ta."ProjectId"
where	extract(year from c."Date") = y
		and extract(month from c."Date") = m
group by p."TeamId", dt."Id", e."Id", c."Id", ta."Id") as res
group by res."TeamId", res."EmployeeId", res."EmployeeFirstName", res."EmployeeLastName", 
		res."DayTypeId", res."DayTypeName"
order by res."EmployeeId"'
language sql;

create or replace function workingMonthly(empId int,y int, m int)
returns decimal
as
'select coalesce(sum(t."Hours"), 0)::decimal as workingMonthly
from "Employees" as e
    join "Calendar" as c on c."EmployeeId"=e."Id"
    join "Tasks" as t on t."DayId"=c."Id"
where
extract(year from c."Date") = y
    and extract(month from c."Date") = m
    and e."Id"= empId'
language sql;

create or replace function workingYearly(empId int,y int)
returns decimal
as
'select coalesce(sum(t."Hours"), 0)::decimal as workingYearly
from "Employees" as e
    join "Calendar" as c on c."EmployeeId"=e."Id"
    join "Tasks" as t on t."DayId"=c."Id"
where extract(year from c."Date") = y
      and e."Id"= empId'
language sql;

create or replace function sickMonthly(empId int, y int, m int)
returns int
as
'select count(c."Id")::int
from "Calendar" as c
join "Employees" as e on c."EmployeeId"=e."Id"
where extract(year from c."Date") = y
      and extract(month from c."Date") = m
      and e."Id"=empId and c."DayTypeId"=5'
language sql;

create or replace function sickYearly(empId int, y int)
returns int
as
'select count(c."Id")::int
from "Calendar" as c
join "Employees" as e on c."EmployeeId"=e."Id"
where extract(year from c."Date") = y
      and e."Id"=empId and c."DayTypeId"=5'
language sql;

create or replace function sickByMonths(empId int, y int)
returns table (m int)
as
'select extract(month from c."Date")::int
from "Calendar" as c
join "Employees" as e on c."EmployeeId"=e."Id"
where extract(year from c."Date") = y
      and e."Id"=empId and c."DayTypeId"=5
group by extract(month from c."Date")'
language sql;

create or replace function personalDashboard(empId int, y int, m int)
returns table (empId int, empName text, workingMonthly dec, workingYearly dec, sickMonthly int, sickYearly int)
as 
'select distinct e."Id", e."FirstName" || '' '' || e."LastName" as "Name",
coalesce(workingMonthly(empId, y, m),0)::decimal as "WorkingMonthly",
workingYearly(empId, y)::decimal as "WorkingYearly",
sickMonthly(empId, y, m)::int as "SickMonthly",
sickYearly(empId, y)::int as "SickYearly"
from "Employees" as e
join "Calendar" as c on c."EmployeeId"=e."Id"
where e."Id"=empId'
language sql;

create or replace function GetMemberPTOHours(tId int, y int, mon int)
returns table(memId int, PtoHours bigint)
as 'select m."Id", (count(c."DayTypeId")*8) as "PTO"
from "Calendar" as c join "Employees" as e on c."EmployeeId"=e."Id"
join "Members" as m on e."Id"= m."EmployeeId"
join "Teams" as t on m."TeamId"=t."Id"
where extract(year from c."Date")=y and extract(month from c."Date")= mon and c."DayTypeId" <> 1
      and t."Id"=tId
group by m."Id"'
language sql;

create or replace function TeamDashboard(tId int, y int, m int)
returns table(employeeId int, memberName text, workingHours dec)
as 'select res."EmployeeId", res."EmployeeName", sum(res."Hours")::dec from
(select distinct on (a."Id") 
        me."Id" as "EmployeeId",
        e."FirstName" || '' '' || e."LastName" as "EmployeeName",
        a."Hours" as "Hours"
from    "Projects" as p 
        join "Teams" as te on p."TeamId" = te."Id"
        join "Members" as me on te."Id" = me."TeamId" 
        join "Employees" as e on e."Id" = me."EmployeeId"
        join "Calendar" as c on e."Id" = c."EmployeeId"
        join "Tasks" as a on c."Id" = a."DayId"
where   extract(year from c."Date") = y
        and extract(month from c."Date") = m
        and te."Id" = tId) as res
group by res."EmployeeId", res."EmployeeName"'
language sql;

create or replace function CountProjects(tId int, y int, m int)
returns table(projectId int)
as 'select p."Id"
from    "Projects" as p 
        join "Teams" as te on p."TeamId" = te."Id"
        join "Members" as me on te."Id" = me."TeamId" 
        join "Employees" as e on e."Id" = me."EmployeeId"
        join "Calendar" as c on e."Id" = c."EmployeeId"
        join "Tasks" as a on c."Id" = a."DayId"
where   extract(year from c."Date") = y
        and extract(month from c."Date") = m
        and te."Id" = tId
group by p."Id"'
language sql;

create or replace function GetMemberOvertimeHours(tId int, y int, m int)
returns table(memberId int, overtime dec)
as 'select  res."MemberId", sum(res."Overtime") as "Overtime"
from (select  me."Id" as "MemberId", sum(a."Hours" - 8) as "Overtime"
from    "Teams" as te join "Members" as me on te."Id" = me."TeamId"
        join "Employees" as e on me."EmployeeId" = e."Id"
        join "Calendar" as c on e."Id" = c."EmployeeId"
        join "Tasks" as a on c."Id" = a."DayId"
where   extract(year from c."Date") = y
        and extract(month from c."Date") = m
        and extract(isodow from c."Date") < 6  
        and a."Hours" > 8
        and te."Id" = tId
group by me."Id"
union 
select  me."Id" as "MemberId", sum(a."Hours") as "Overtime"
from    "Teams" as te join "Members" as me on te."Id" = me."TeamId"
        join "Employees" as e on me."EmployeeId" = e."Id"
        join "Calendar" as c on e."Id" = c."EmployeeId"
        join "Tasks" as a on c."Id" = a."DayId"
where   extract(year from c."Date") = y
        and extract(month from c."Date") = m
        and extract(isodow from c."Date") >= 6
        and te."Id" = tId
group by me."Id") as res
group by res."MemberId"'
language sql;

create or replace function DateMonth(tId int, y int, m int)
returns table(eId int, eName text, n dec)
as 'select me."Id", e."FirstName" || '' '' || e."LastName" as "EmployeeName", count(c."Date")::dec
from "Calendar" as c
      join "Employees" as e on c."EmployeeId" = e."Id"
      join "Members" as me on me."EmployeeId" = e."Id"
      join "Teams" as t on t."Id" = me."TeamId"
where extract(year from c."Date") = y
        and extract(month from c."Date") = m
        and t."Id" = tId
        and 
        EXTRACT(ISODOW FROM c."Date") IN (1, 2, 3, 4, 5)
 group by me."Id", e."FirstName", e."LastName"'
language sql;

create or replace function SickDays(empId int, y int)
returns table (sickDay date)
as 
'select  c."Date"::date as "Date"
from 	"Calendar" as c
where 	c."EmployeeId" = empId
		and extract(year from c."Date") = y
		and c."DayTypeId" = 5
order by c."Date"'
language sql;
