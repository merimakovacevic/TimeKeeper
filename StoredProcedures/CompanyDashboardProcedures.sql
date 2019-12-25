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


/*Role, Employee, Team, Project utilization*/
select 	res."EmployeeId", res."TeamId", res."TeamName",
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
where	extract(year from c."Date") = 2018
		and extract(month from c."Date") = 12) as res
group by 	res."RoleId", res."RoleName", res."RoleHourlyPrice", res."RoleMonthlyPrice", 
			res."EmployeeId", res."TeamId", res."TeamName",
			res."ProjectId", res."ProjectName", res."ProjectAmount",
			res."ProjectPricingId", res."ProjectPricingName"
order by res."EmployeeId"





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


/*Overtime Hours for an employee for a month*/
select  res."EmployeeId", res."FirstName", res."LastName", sum("Overtime") as "Overtime" from
(select e."Id" as "EmployeeId", e."FirstName", e."LastName", c."Id" as "DayId", c."Date", coalesce(sum(t."Hours"), 0) as "WorkingHours",
case 	when (c."DayTypeId" = 1) and (extract(isodow from c."Date") >= 6) then (coalesce(sum(t."Hours"), 0))
		when (c."DayTypeId" = 1) and (extract(isodow from c."Date") < 6) and (coalesce(sum(t."Hours"), 0) > 8) then (coalesce(sum(t."Hours"), 0) - 8)
		else 0 end
		as "Overtime"
from	"Employees" as e join "Calendar" as c on e."Id" = c."EmployeeId"
		join "Tasks" as t on c."Id" = t."DayId"
where	extract(year from c."Date") = 2018
		and extract(month from c."Date") = 12		
group by e."Id", c."Id") as res
group by res."EmployeeId", res."FirstName", res."LastName"
order by res."EmployeeId"

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


/*All hours summed by employee and day type with the sum column showing total daytype hours
 * In case of non-working hours, teamId is 0*/
select 	res."TeamId", res."EmployeeId", res."EmployeeFirstName", res."EmployeeLastName", 
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
where	extract(year from c."Date") = 2019
		and extract(month from c."Date") = 7
group by p."TeamId", dt."Id", e."Id", c."Id", ta."Id") as res
group by res."TeamId", res."EmployeeId", res."EmployeeFirstName", res."EmployeeLastName", 
		res."DayTypeId", res."DayTypeName"
order by res."EmployeeId"
