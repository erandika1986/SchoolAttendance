(self.webpackChunkoreva=self.webpackChunkoreva||[]).push([[937],{4937:function(e,t,i){"use strict";i.r(t),i.d(t,{AttendanceModule:function(){return ce}});var n=i(1116),s=i(8569),a=i(1041),r=i(5366);let o=(()=>{class e{}return e.\u0275fac=function(t){return new(t||e)},e.\u0275prov=r.Yz7({token:e,factory:e.\u0275fac}),e})();var l=i(2501),d=i.n(l),c=i(2693),g=i(529);let h=(()=>{class e{constructor(e){this.httpClient=e}getMySubjectAttendance(e,t,i,n,s,a,r){return this.httpClient.get(g.N.apiUrl+"Attendance/getMySubjectAttendance",{params:(new c.LE).set("searchText",e).set("currentPage",t.toString()).set("pageSize",i.toString()).set("academicYearId",n.toString()).set("gradeId",s.toString()).set("classId",a.toString()).set("subjectId",r.toString())})}getAttendanceListTeacherDropdownMasterData(){return this.httpClient.get(g.N.apiUrl+"Attendance/getAttendanceListTeacherDropdownMasterData")}getStartAndEndTime(e){return this.httpClient.post(g.N.apiUrl+"Attendance/getStartAndEndTime",e)}getAttendanceDetailForSubjectClassById(e){return this.httpClient.get(g.N.apiUrl+"Attendance/getAttendanceDetailForSubjectClassById/"+e)}getAttendanceDetailForClassSubject(e){return this.httpClient.post(g.N.apiUrl+"Attendance/getAttendanceDetailForClassSubject",e)}saveAttendanceDetailForClassSubject(e){return this.httpClient.post(g.N.apiUrl+"Attendance/saveAttendanceDetailForClassSubject",e)}}return e.\u0275fac=function(t){return new(t||e)(r.LFG(c.eN))},e.\u0275prov=r.Yz7({token:e,factory:e.\u0275fac,providedIn:"root"}),e})();var u=i(8511),m=i(893),p=i(1269),Z=i(1709),f=i(4686),A=i(2290);const T=function(e,t,i){return{"p-inputswitch p-component":!0,"p-inputswitch-checked":e,"p-disabled":t,"p-focus":i}},b={provide:a.JU,useExisting:(0,r.Gpc)(()=>v),multi:!0};let v=(()=>{class e{constructor(e){this.cd=e,this.onChange=new r.vpe,this.checked=!1,this.focused=!1,this.onModelChange=()=>{},this.onModelTouched=()=>{}}onClick(e,t){this.disabled||this.readonly||(e.preventDefault(),this.toggle(e),t.focus())}onInputChange(e){this.readonly||this.updateModel(e,e.target.checked)}toggle(e){this.updateModel(e,!this.checked)}updateModel(e,t){this.checked=t,this.onModelChange(this.checked),this.onChange.emit({originalEvent:e,checked:this.checked})}onFocus(e){this.focused=!0}onBlur(e){this.focused=!1,this.onModelTouched()}writeValue(e){this.checked=e,this.cd.markForCheck()}registerOnChange(e){this.onModelChange=e}registerOnTouched(e){this.onModelTouched=e}setDisabledState(e){this.disabled=e,this.cd.markForCheck()}}return e.\u0275fac=function(t){return new(t||e)(r.Y36(r.sBO))},e.\u0275cmp=r.Xpm({type:e,selectors:[["p-inputSwitch"]],inputs:{style:"style",styleClass:"styleClass",tabindex:"tabindex",inputId:"inputId",name:"name",disabled:"disabled",readonly:"readonly",ariaLabelledBy:"ariaLabelledBy"},outputs:{onChange:"onChange"},features:[r._Bn([b])],decls:5,vars:15,consts:[[3,"ngClass","ngStyle","click"],[1,"p-hidden-accessible"],["type","checkbox","role","switch",3,"checked","disabled","change","focus","blur"],["cb",""],[1,"p-inputswitch-slider"]],template:function(e,t){if(1&e){const e=r.EpF();r.TgZ(0,"div",0),r.NdJ("click",function(i){r.CHM(e);const n=r.MAs(3);return t.onClick(i,n)}),r.TgZ(1,"div",1),r.TgZ(2,"input",2,3),r.NdJ("change",function(e){return t.onInputChange(e)})("focus",function(e){return t.onFocus(e)})("blur",function(e){return t.onBlur(e)}),r.qZA(),r.qZA(),r._UZ(4,"span",4),r.qZA()}2&e&&(r.Tol(t.styleClass),r.Q6J("ngClass",r.kEZ(11,T,t.checked,t.disabled,t.focused))("ngStyle",t.style),r.xp6(2),r.Q6J("checked",t.checked)("disabled",t.disabled),r.uIk("id",t.inputId)("name",t.name)("tabindex",t.tabindex)("aria-checked",t.checked)("aria-labelledby",t.ariaLabelledBy))},directives:[n.mk,n.PC],styles:['.p-inputswitch{position:relative;display:inline-block;-webkit-user-select:none;-ms-user-select:none;user-select:none}.p-inputswitch-slider{position:absolute;cursor:pointer;top:0;left:0;right:0;bottom:0}.p-inputswitch-slider:before{position:absolute;content:"";top:50%}'],encapsulation:2,changeDetection:0}),e})(),w=(()=>{class e{}return e.\u0275fac=function(t){return new(t||e)},e.\u0275mod=r.oAB({type:e}),e.\u0275inj=r.cJS({imports:[[n.ez]]}),e})();function q(e,t){if(1&e&&(r.TgZ(0,"option",42),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function F(e,t){if(1&e&&(r.TgZ(0,"option",42),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function I(e,t){if(1&e&&(r.TgZ(0,"option",42),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function C(e,t){if(1&e&&(r.TgZ(0,"option",42),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function N(e,t){if(1&e&&(r.TgZ(0,"option",42),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function S(e,t){1&e&&(r.TgZ(0,"small",43),r._uU(1," Please enter your lesson details"),r.qZA())}function x(e,t){1&e&&(r.TgZ(0,"tr"),r.TgZ(1,"th",44),r._uU(2,"#"),r.qZA(),r.TgZ(3,"th",45),r._uU(4,"Index No"),r.qZA(),r.TgZ(5,"th"),r._uU(6,"Student Name"),r.qZA(),r.TgZ(7,"th"),r._uU(8,"Is Present"),r.qZA(),r.qZA())}function D(e,t){1&e&&r._UZ(0,"img",50)}function y(e,t){1&e&&r._UZ(0,"img",51)}const j=function(){return{standalone:!0}};function Y(e,t){if(1&e&&(r.TgZ(0,"tr"),r.TgZ(1,"td"),r._uU(2),r.qZA(),r.TgZ(3,"td"),r._uU(4),r.qZA(),r.TgZ(5,"td"),r.YNc(6,D,1,0,"img",46),r.YNc(7,y,1,0,"img",47),r.TgZ(8,"span",48),r._uU(9),r.qZA(),r.qZA(),r.TgZ(10,"td"),r.TgZ(11,"p-inputSwitch",49),r.NdJ("ngModelChange",function(e){return t.$implicit.isPresent=e}),r.qZA(),r.qZA(),r.qZA()),2&e){const e=t.$implicit,i=t.rowIndex;r.xp6(2),r.hij(" ",i+1," "),r.xp6(2),r.hij(" ",e.indexNo," "),r.xp6(2),r.Q6J("ngIf","M"==e.gender),r.xp6(1),r.Q6J("ngIf","F"==e.gender),r.xp6(2),r.Oqu(e.studentName),r.xp6(2),r.Q6J("ngModel",e.isPresent)("ngModelOptions",r.DdM(7,j))}}const U=function(){return{width:"100%"}};let J=(()=>{class e{constructor(e,t,i,n,s,a,r){this.attendanceService=e,this.formBuilder=t,this.modalService=i,this.activateRoute=n,this.router=s,this.dropdownService=a,this.spinner=r,this.id=0,this.academicYears=[],this.grades=[],this.classes=[],this.subjects=[],this.softwares=[{id:"Zoom",name:"Zoom"},{id:"Microsoft Team",name:"Microsoft Team"},{id:"Google Meet",name:"Google Meet"},{id:"Skype",name:"Skype"}],this.classTypes=[{id:!1,name:"No"},{id:!0,name:"Yes"}],this.filter=new o,this.data=[],this.date=new Date,this.createFilterForm()}ngOnInit(){this.activateRoute.params.subscribe(e=>{this.id=+e.id,0==this.id?(this.spinner.show(),this.loadGrades()):(this.spinner.show(),this.generateExistingAttendanceSheet())})}createFilterForm(){this.filterForm=this.formBuilder.group({id:new a.NI(this.id),year:new a.NI(this.date.getFullYear()),month:new a.NI(this.date.getMonth()+1),day:new a.NI(this.date.getDate()),gradeId:new a.NI(0),classId:new a.NI(0),subjectId:new a.NI(0),startHour:new a.NI(0),startMin:new a.NI(0),endHour:new a.NI(0),endMin:new a.NI(0),isExtraClass:new a.NI(!1),lessonDetails:new a.NI("",a.kI.required),softwareName:new a.NI("Zoom"),timeSlotId:new a.NI(0),studentsAttendance:this.formBuilder.array([]),selectedDate:new a.NI(this.date),startTime:new a.NI(this.date),endTime:new a.NI(this.date)})}generateExistingAttendanceSheet(){this.attendanceService.getAttendanceDetailForSubjectClassById(this.id).subscribe(e=>{this.spinner.hide(),this.data=e.studentsAttendance,this.date=new Date(e.year,e.month-1,e.day,0,0,0);let t=new Date(e.year,e.month-1,e.day,e.startHour,e.startMin,0),i=new Date(e.year,e.month-1,e.day,e.endHour,e.endMin,0);this.filterForm=this.formBuilder.group({id:new a.NI(this.id),year:new a.NI(this.date.getFullYear()),month:new a.NI(this.date.getMonth()-1),day:new a.NI(this.date.getDate()),gradeId:new a.NI(e.gradeId),classId:new a.NI(e.classId),subjectId:new a.NI(e.subjectId),startHour:new a.NI(e.startHour),startMin:new a.NI(e.startMin),endHour:new a.NI(e.endHour),endMin:new a.NI(e.endMin),isExtraClass:new a.NI(e.isExtraClass),lessonDetails:new a.NI(e.lessonDetails,a.kI.required),softwareName:new a.NI(e.softwareName),timeSlotId:new a.NI(e.timeSlotId),studentsAttendance:this.formBuilder.array([]),selectedDate:new a.NI(this.date),startTime:new a.NI(t),endTime:new a.NI(i)}),console.log("===================================="),console.log("disabled"),console.log("===================================="),this.spinner.show(),this.loadGrades()},e=>{this.spinner.hide()}),this.filterForm.controls.selectedDate.disable(),this.filterForm.get("gradeId").disable(),this.filterForm.get("classId").disable(),this.filterForm.get("subjectId").disable()}loadGrades(){this.dropdownService.getTeachGradesForLoggedInUser(this.date.getFullYear()).subscribe(e=>{this.grades=e,this.grades.length>0&&(0==this.id&&this.filterForm.get("gradeId").setValue(this.grades[0].id),this.loadClasses()),this.spinner.hide()},e=>{this.spinner.hide()})}loadClasses(){this.dropdownService.getAssignedClassForLoggedInUser(this.date.getFullYear(),this.gradeId).subscribe(e=>{this.classes=e,this.classes.length>0&&(0==this.id&&this.filterForm.get("classId").setValue(this.classes[0].id),this.loadSubject()),this.spinner.hide()},e=>{this.spinner.hide()})}loadSubject(){this.dropdownService.getAssignedClassSubjectForLoggedInUser(this.classId).subscribe(e=>{this.subjects=e,this.subjects.length>0&&0==this.id&&(this.filterForm.get("subjectId").setValue(this.subjects[0].id),this.setStartDateEndDate()),this.spinner.hide()},e=>{this.spinner.hide()})}onDateChanged(e){this.spinner.show(),this.filterForm.get("year").setValue(this.selectedDate.getFullYear()),this.filterForm.get("month").setValue(this.selectedDate.getMonth()+1),this.filterForm.get("day").setValue(this.selectedDate.getDate()),this.setStartDateEndDate()}onStartTimeChanged(e){this.filterForm.get("startHour").setValue(this.startTime.getHours()),this.filterForm.get("startMin").setValue(this.startTime.getMinutes())}onEndTimeChanged(e){this.filterForm.get("endHour").setValue(this.endTime.getHours()),this.filterForm.get("endMin").setValue(this.endTime.getHours())}onGradeFilterChanged(e){this.spinner.show(),this.loadClasses()}onClassFilterChanged(e){this.spinner.show(),this.loadSubject()}onSubjectChanged(e){this.spinner.show(),this.setStartDateEndDate()}setStartDateEndDate(){let e=new o;e.classId=this.classId,e.subjectId=this.subjectId,e.year=this.selectedDate.getFullYear(),e.month=this.selectedDate.getMonth()+1,e.day=this.selectedDate.getDate(),this.attendanceService.getStartAndEndTime(e).subscribe(e=>{let t=new Date(e.year,e.month-1,e.day,e.startHour,e.startMin,0),i=new Date(e.year,e.month-1,e.day,e.endHour,e.endMin,0);this.filterForm.get("startHour").setValue(e.startHour),this.filterForm.get("startMin").setValue(e.startMin),this.filterForm.get("endHour").setValue(e.endHour),this.filterForm.get("endMin").setValue(e.endMin),this.filterForm.get("startTime").setValue(t),this.filterForm.get("endTime").setValue(i),this.loadSubjectAttendance()},e=>{this.spinner.hide()})}loadSubjectAttendance(){this.attendanceService.getAttendanceDetailForClassSubject(this.filterForm.getRawValue()).subscribe(e=>{e.id>0&&(this.filterForm.get("id").setValue(e.id),this.filterForm.get("lessonDetails").setValue(e.lessonDetails),this.filterForm.get("softwareName").setValue(e.softwareName),this.filterForm.get("isExtraClass").setValue(e.isExtraClass)),this.filterForm.get("timeSlotId").setValue(e.timeSlotId),this.data=e.studentsAttendance,this.spinner.hide()},e=>{this.spinner.hide()})}save(){let e=this.filterForm.getRawValue();e.studentsAttendance=this.data,this.attendanceService.saveAttendanceDetailForClassSubject(e).subscribe(e=>{e.isSuccess?(d().fire({icon:"success",title:"Done",text:e.message}),this.router.navigate(["/attendance/attendance-list"])):d().fire({icon:"error",title:"Failed",text:e.message})},e=>{d().fire({icon:"error",title:"Failed",text:"Network error has been occured. Please try again."})})}reset(){}get selectedDate(){return this.filterForm.get("selectedDate").value}get startTime(){return this.filterForm.get("startTime").value}get endTime(){return this.filterForm.get("endTime").value}get gradeId(){return this.filterForm.get("gradeId").value}get classId(){return this.filterForm.get("classId").value}get subjectId(){return this.filterForm.get("subjectId").value}get studentArray(){return this.filterForm.get("studentsAttendance")}}return e.\u0275fac=function(t){return new(t||e)(r.Y36(h),r.Y36(a.qu),r.Y36(u.FF),r.Y36(s.gz),r.Y36(s.F0),r.Y36(m.V),r.Y36(p.t2))},e.\u0275cmp=r.Xpm({type:e,selectors:[["app-subject-attendance"]],decls:93,vars:27,consts:[[1,"main-content"],[1,"breadcrumb","breadcrumb-style"],[1,"breadcrumb-item"],[1,"page-title","m-b-0"],[1,"breadcrumb-item","bcrumb-1"],["routerLink","/attendance/attendance-list"],[1,"fas","fa-home","font-17"],[1,"section-body"],[1,"row","clearfix"],[1,"col-lg-12","col-md-12","col-sm-12","col-xs-12"],[1,"card"],[3,"formGroup"],[1,"p-10"],[1,"row"],[1,"row","m-0"],[1,"col-12","p-0"],[1,"ngxTableHeader"],[1,"table-title"],[1,"col-lg-12","col-md-12","col-sm-12","col-xs-12","p-0"],[1,"col-lg-3","col-md-3","col-sm-12","col-xs-12"],[1,"form-group"],["for","date"],["id","date","inputId","basic","formControlName","selectedDate",1,"form-control",3,"inputStyle","onSelect"],["formControlName","gradeId",1,"form-select","form-control",2,"width","100% !important",3,"change"],[3,"value",4,"ngFor","ngForOf"],["formControlName","classId",1,"form-select","form-control",3,"change"],["formControlName","subjectId",1,"form-select","form-control",3,"change"],["for","startTime"],["id","startTime","inputId","startTime","formControlName","startTime",1,"form-control",3,"inputStyle","timeOnly","onSelect"],["for","endTime"],["id","endTime","inputId","endTime","formControlName","endTime",1,"form-control",3,"inputStyle","timeOnly","onSelect"],["formControlName","softwareName",1,"form-select","form-control"],["formControlName","isExtraClass",1,"form-select","form-control"],["placeholder","Type your lession description here.","formControlName","lessonDetails","required","",1,"form-control"],["class","form-text text-danger",4,"ngIf"],["styleClass","p-datatable-striped",3,"value","responsive"],["pTemplate","header"],["pTemplate","body"],[1,"card-footer","text-center"],[1,"col-lg-6","col-md-6","col-sm-12","col-xs-12","ps-3","pe-3","pt-3"],["type","button",1,"btn","btn-primary",2,"width","100%",3,"disabled","click"],["type","button",1,"btn","btn-secondary",2,"width","100%",3,"click"],[3,"value"],[1,"form-text","text-danger"],[2,"width","100px"],[2,"width","150px"],["src","assets/images/student-m.png","width","32","style","vertical-align: middle",4,"ngIf"],["src","assets/images/student-f.png","width","32","height","32","style","vertical-align: middle",4,"ngIf"],[1,"image-text"],[3,"ngModel","ngModelOptions","ngModelChange"],["src","assets/images/student-m.png","width","32",2,"vertical-align","middle"],["src","assets/images/student-f.png","width","32","height","32",2,"vertical-align","middle"]],template:function(e,t){1&e&&(r.TgZ(0,"section",0),r.TgZ(1,"ul",1),r.TgZ(2,"li",2),r.TgZ(3,"h5",3),r._uU(4,"Attendance"),r.qZA(),r.qZA(),r.TgZ(5,"li",4),r.TgZ(6,"a",5),r._UZ(7,"i",6),r.qZA(),r.qZA(),r.TgZ(8,"li",2),r._uU(9,"Subject Attendance"),r.qZA(),r.qZA(),r.TgZ(10,"div",7),r.TgZ(11,"div",8),r.TgZ(12,"div",9),r.TgZ(13,"div",10),r.TgZ(14,"form",11),r.TgZ(15,"div",12),r.TgZ(16,"div",13),r.TgZ(17,"div",9),r.TgZ(18,"div",14),r.TgZ(19,"div",15),r.TgZ(20,"div",16),r.TgZ(21,"div",17),r.TgZ(22,"h2"),r.TgZ(23,"strong"),r._uU(24,"Subject Attendance"),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.TgZ(25,"div",18),r.TgZ(26,"div",13),r.TgZ(27,"div",19),r.TgZ(28,"div",20),r.TgZ(29,"label",21),r._uU(30,"Date"),r.qZA(),r.TgZ(31,"p-calendar",22),r.NdJ("onSelect",function(e){return t.onDateChanged(e)}),r.qZA(),r.qZA(),r.qZA(),r.TgZ(32,"div",19),r.TgZ(33,"div",20),r.TgZ(34,"label"),r._uU(35,"Grade"),r.qZA(),r.TgZ(36,"select",23),r.NdJ("change",function(e){return t.onGradeFilterChanged(e.target.value)}),r.YNc(37,q,2,2,"option",24),r.qZA(),r.qZA(),r.qZA(),r.TgZ(38,"div",19),r.TgZ(39,"div",20),r.TgZ(40,"label"),r._uU(41,"Class"),r.qZA(),r.TgZ(42,"select",25),r.NdJ("change",function(e){return t.onClassFilterChanged(e.target.value)}),r.YNc(43,F,2,2,"option",24),r.qZA(),r.qZA(),r.qZA(),r.TgZ(44,"div",19),r.TgZ(45,"div",20),r.TgZ(46,"label"),r._uU(47,"Subject"),r.qZA(),r.TgZ(48,"select",26),r.NdJ("change",function(e){return t.onSubjectChanged(e.target.value)}),r.YNc(49,I,2,2,"option",24),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.TgZ(50,"div",13),r.TgZ(51,"div",19),r.TgZ(52,"div",20),r.TgZ(53,"label",27),r._uU(54,"Start Time"),r.qZA(),r.TgZ(55,"p-calendar",28),r.NdJ("onSelect",function(e){return t.onStartTimeChanged(e)}),r.qZA(),r.qZA(),r.qZA(),r.TgZ(56,"div",19),r.TgZ(57,"div",20),r.TgZ(58,"label",29),r._uU(59,"End Time"),r.qZA(),r.TgZ(60,"p-calendar",30),r.NdJ("onSelect",function(e){return t.onEndTimeChanged(e)}),r.qZA(),r.qZA(),r.qZA(),r.TgZ(61,"div",19),r.TgZ(62,"div",20),r.TgZ(63,"label"),r._uU(64,"Software"),r.qZA(),r.TgZ(65,"select",31),r.YNc(66,C,2,2,"option",24),r.qZA(),r.qZA(),r.qZA(),r.TgZ(67,"div",19),r.TgZ(68,"div",20),r.TgZ(69,"label"),r._uU(70,"Is Extra Class"),r.qZA(),r.TgZ(71,"select",32),r.YNc(72,N,2,2,"option",24),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.TgZ(73,"div",13),r.TgZ(74,"div",9),r.TgZ(75,"div",20),r.TgZ(76,"label"),r._uU(77,"Lesson Description"),r.qZA(),r._UZ(78,"textarea",33),r.YNc(79,S,2,0,"small",34),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.TgZ(80,"div",13),r.TgZ(81,"div",9),r.TgZ(82,"p-table",35),r.YNc(83,x,9,0,"ng-template",36),r.YNc(84,Y,12,8,"ng-template",37),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.TgZ(85,"div",38),r.TgZ(86,"div",13),r.TgZ(87,"div",39),r.TgZ(88,"button",40),r.NdJ("click",function(){return t.save()}),r._uU(89," Save Attendance "),r.qZA(),r.qZA(),r.TgZ(90,"div",39),r.TgZ(91,"button",41),r.NdJ("click",function(){return t.reset()}),r._uU(92," Reset Attendance "),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA()),2&e&&(r.xp6(14),r.Q6J("formGroup",t.filterForm),r.xp6(17),r.Akn(r.DdM(21,U)),r.Q6J("inputStyle",r.DdM(22,U)),r.xp6(6),r.Q6J("ngForOf",t.grades),r.xp6(6),r.Q6J("ngForOf",t.classes),r.xp6(6),r.Q6J("ngForOf",t.subjects),r.xp6(6),r.Akn(r.DdM(23,U)),r.Q6J("inputStyle",r.DdM(24,U))("timeOnly",!0),r.xp6(5),r.Akn(r.DdM(25,U)),r.Q6J("inputStyle",r.DdM(26,U))("timeOnly",!0),r.xp6(6),r.Q6J("ngForOf",t.softwares),r.xp6(6),r.Q6J("ngForOf",t.classTypes),r.xp6(7),r.Q6J("ngIf",!t.filterForm.get("lessonDetails").valid&&t.filterForm.get("lessonDetails").touched),r.xp6(3),r.Q6J("value",t.data)("responsive",!0),r.xp6(6),r.Q6J("disabled",!t.filterForm.valid))},directives:[s.yS,a._Y,a.JL,a.sg,Z.f,a.JJ,a.u,a.EJ,n.sg,a.Fj,a.Q7,n.O5,f.iA,A.jx,a.YN,a.Kr,v,a.On],styles:[".p-calendar .p-inputtext{flex:0 0 auto;width:100%;margin-top:-11px;margin-left:-16px;margin-right:-16px}"]}),e})(),M=(()=>{class e{constructor(e){this.httpClient=e}downloadClassAttendanceForAllSubjects(e){return this.httpClient.post(g.N.apiUrl+"Report/downloadClassAttendanceForAllSubjects",e,{headers:{filedownload:""},observe:"events",reportProgress:!0})}downloadClassAttendanceForSelectedSubject(e){return this.httpClient.post(g.N.apiUrl+"Report/downloadClassAttendanceForSelectedSubject",e,{headers:{filedownload:""},observe:"events",reportProgress:!0})}generateZonalReportForSelectedClass(e){return this.httpClient.post(g.N.apiUrl+"Report/generateZonalReportForSelectedClass",e,{headers:{filedownload:""},observe:"events",reportProgress:!0})}getTeacherClassMasterData(){return this.httpClient.get(g.N.apiUrl+"Report/getTeacherClassMasterData")}}return e.\u0275fac=function(t){return new(t||e)(r.LFG(c.eN))},e.\u0275prov=r.Yz7({token:e,factory:e.\u0275fac,providedIn:"root"}),e})();function _(e,t){if(1&e&&(r.TgZ(0,"option",30),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function k(e,t){if(1&e&&(r.TgZ(0,"option",30),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function Q(e,t){if(1&e&&(r.TgZ(0,"option",30),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function V(e,t){if(1&e&&(r.TgZ(0,"option",30),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}const G=function(){return{width:"100%"}},R=function(){return[0,2,3,4,5,6]},H=function(){return[0,1,2,3,4,6]};let E=(()=>{class e{constructor(e,t,i,n,s){this.attendanceService=e,this.reportService=t,this.router=i,this.dropdownService=n,this.spinner=s,this.reports=[{id:1,name:"Class Week Attendance Report"},{id:2,name:"Subject Attendance Report"},{id:2,name:"Class Weekly Zonal Report"}],this.academicYears=[],this.grades=[],this.classes=[],this.subjects=[],this.downloadPercentage=0,this.filterForm=this.createFilterForm()}ngOnInit(){this.getTeacherMasterData()}createFilterForm(){return new a.cw({selectedReportId:new a.NI(1),fromYear:new a.NI(0),fromMonth:new a.NI(0),fromDay:new a.NI(0),toYear:new a.NI(0),toMonth:new a.NI(0),toDay:new a.NI(0),fromDate:new a.NI(new Date),toDate:new a.NI(new Date),selectedYearId:new a.NI(0),selectedGradeId:new a.NI(0),selectedClassId:new a.NI(0),selectedSubjectId:new a.NI(0)})}getTeacherMasterData(){this.reportService.getTeacherClassMasterData().subscribe(e=>{this.classes=e.classes,this.academicYears=e.academicYears,this.grades=e.grades,this.filterForm.get("selectedYearId").setValue(e.selectedYearId),this.filterForm.get("selectedGradeId").setValue(e.selectedGradeId),this.filterForm.get("selectedClassId").setValue(e.selectedClassId),this.filterForm.get("fromDate").setValue(new Date(e.fromYear,e.fromMonth-1,e.fromDay,0,0,0)),this.filterForm.get("toDate").setValue(new Date(e.toYear,e.toMonth-1,e.toDay,0,0,0)),this.setFromandToDate(),this.filterForm.get("toDate").disable(),"Admin"!=e.role&&(this.filterForm.get("selectedYearId").disable(),this.filterForm.get("selectedGradeId").disable(),this.filterForm.get("selectedClassId").disable())},e=>{})}onSelectedReportChanged(e){}onAcademicYearFilterChanged(e){}onGradeFilterChanged(e){}onClassFilterChanged(e){}onSubjectFilterChanged(e){}onFromDateChanged(e){let t=new Date(this.fromDate.getTime()+3456e5);this.toDate.setDate(this.fromDate.getDate()+4),this.filterForm.get("toDate").setValue(t),this.setFromandToDate()}setFromandToDate(){this.filterForm.get("fromYear").setValue(this.fromDate.getFullYear()),this.filterForm.get("fromMonth").setValue(this.fromDate.getMonth()+1),this.filterForm.get("fromDay").setValue(this.fromDate.getDate()),this.filterForm.get("toYear").setValue(this.toDate.getFullYear()),this.filterForm.get("toMonth").setValue(this.toDate.getMonth()+1),this.filterForm.get("toDay").setValue(this.toDate.getDate())}onToDateChanged(e){}generateReport(){this.isDownloading=!0,this.spinner.show(),this.reportService.downloadClassAttendanceForAllSubjects(this.filterForm.getRawValue()).subscribe(e=>{if(e.type===c.dt.Response)if(204==e.status)this.isDownloading=!1,this.downloadPercentage=0,this.spinner.hide();else{let t=e.headers.get("content-disposition");const i=URL.createObjectURL(e.body),n=document.createElement("a");n.href=i,n.download=this.parseFilenameFromContentDisposition(t),document.body.appendChild(n),n.click(),document.body.removeChild(n),URL.revokeObjectURL(i),this.isDownloading=!1,this.downloadPercentage=0,this.spinner.hide()}},e=>{this.spinner.hide(),this.isDownloading=!1,this.downloadPercentage=0})}parseFilenameFromContentDisposition(e){if(!e)return null;let t=/filename="(.*?)"/g.exec(e);return t&&t.length>1?t[1]:null}get toDate(){return this.filterForm.get("toDate").value}get fromDate(){return this.filterForm.get("fromDate").value}}return e.\u0275fac=function(t){return new(t||e)(r.Y36(h),r.Y36(M),r.Y36(s.F0),r.Y36(m.V),r.Y36(p.t2))},e.\u0275cmp=r.Xpm({type:e,selectors:[["app-attendance-reports"]],decls:67,vars:19,consts:[[1,"main-content"],[1,"breadcrumb","breadcrumb-style"],[1,"breadcrumb-item"],[1,"page-title","m-b-0"],[1,"breadcrumb-item","bcrumb-1"],["routerLink","/admin/teacher"],[1,"fas","fa-home","font-17"],[1,"section-body"],[1,"row","clearfix"],[1,"col-lg-12","col-md-12","col-sm-12","col-xs-12"],[1,"card"],[1,"p-10"],[1,"row"],[1,"row","m-0"],[3,"formGroup"],[1,"col-12","p-0"],[1,"ngxTableHeader"],[1,"table-title"],[1,"col-lg-12","col-md-12","col-sm-12","col-xs-12","p-0"],[1,"col-lg-3","col-md-3","col-sm-12","col-xs-12"],[1,"form-group"],["formControlName","selectedReportId",1,"form-select","form-control",3,"change"],[3,"value",4,"ngFor","ngForOf"],["formControlName","selectedYearId",1,"form-select","form-control",3,"change"],["formControlName","selectedGradeId",1,"form-select","form-control",3,"change"],["formControlName","selectedClassId",1,"form-select","form-control",3,"change"],["for","date"],["id","date","inputId","basic","formControlName","fromDate",1,"form-control",3,"disabledDays","inputStyle","onSelect"],["id","date","inputId","basic","formControlName","toDate",1,"form-control",3,"disabledDays","inputStyle","onSelect"],["type","button",1,"btn","btn-primary",2,"margin-top","30px",3,"click"],[3,"value"]],template:function(e,t){1&e&&(r.TgZ(0,"section",0),r.TgZ(1,"ul",1),r.TgZ(2,"li",2),r.TgZ(3,"h5",3),r._uU(4,"Attendance"),r.qZA(),r.qZA(),r.TgZ(5,"li",4),r.TgZ(6,"a",5),r._UZ(7,"i",6),r.qZA(),r.qZA(),r.TgZ(8,"li",2),r._uU(9,"Reports"),r.qZA(),r.qZA(),r.TgZ(10,"div",7),r.TgZ(11,"div",8),r.TgZ(12,"div",9),r.TgZ(13,"div",10),r.TgZ(14,"div",11),r.TgZ(15,"div",12),r.TgZ(16,"div",9),r.TgZ(17,"div",13),r.TgZ(18,"form",14),r.TgZ(19,"div",15),r.TgZ(20,"div",16),r.TgZ(21,"div",17),r.TgZ(22,"h2"),r.TgZ(23,"strong"),r._uU(24,"Attendance Reports"),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.TgZ(25,"div",18),r.TgZ(26,"div",12),r.TgZ(27,"div",19),r.TgZ(28,"div",20),r.TgZ(29,"label"),r._uU(30,"Report Name"),r.qZA(),r.TgZ(31,"select",21),r.NdJ("change",function(e){return t.onSelectedReportChanged(e.target.value)}),r.YNc(32,_,2,2,"option",22),r.qZA(),r.qZA(),r.qZA(),r.TgZ(33,"div",19),r.TgZ(34,"div",20),r.TgZ(35,"label"),r._uU(36,"Academic Year"),r.qZA(),r.TgZ(37,"select",23),r.NdJ("change",function(e){return t.onAcademicYearFilterChanged(e.target.value)}),r.YNc(38,k,2,2,"option",22),r.qZA(),r.qZA(),r.qZA(),r.TgZ(39,"div",19),r.TgZ(40,"div",20),r.TgZ(41,"label"),r._uU(42,"Grade"),r.qZA(),r.TgZ(43,"select",24),r.NdJ("change",function(e){return t.onGradeFilterChanged(e.target.value)}),r.YNc(44,Q,2,2,"option",22),r.qZA(),r.qZA(),r.qZA(),r.TgZ(45,"div",19),r.TgZ(46,"div",20),r.TgZ(47,"label"),r._uU(48,"Class"),r.qZA(),r.TgZ(49,"select",25),r.NdJ("change",function(e){return t.onClassFilterChanged(e.target.value)}),r.YNc(50,V,2,2,"option",22),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.TgZ(51,"div",12),r.TgZ(52,"div",19),r.TgZ(53,"div",20),r.TgZ(54,"label",26),r._uU(55,"From Date"),r.qZA(),r.TgZ(56,"p-calendar",27),r.NdJ("onSelect",function(e){return t.onFromDateChanged(e)}),r.qZA(),r.qZA(),r.qZA(),r.TgZ(57,"div",19),r.TgZ(58,"div",20),r.TgZ(59,"label",26),r._uU(60,"To Date"),r.qZA(),r.TgZ(61,"p-calendar",28),r.NdJ("onSelect",function(e){return t.onToDateChanged(e)}),r.qZA(),r.qZA(),r.qZA(),r.TgZ(62,"div",19),r.TgZ(63,"div",20),r._UZ(64,"label",26),r.TgZ(65,"button",29),r.NdJ("click",function(){return t.generateReport()}),r._uU(66," Generate Report "),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA()),2&e&&(r.xp6(18),r.Q6J("formGroup",t.filterForm),r.xp6(14),r.Q6J("ngForOf",t.reports),r.xp6(6),r.Q6J("ngForOf",t.academicYears),r.xp6(6),r.Q6J("ngForOf",t.grades),r.xp6(6),r.Q6J("ngForOf",t.classes),r.xp6(6),r.Akn(r.DdM(13,G)),r.Q6J("disabledDays",r.DdM(14,R))("inputStyle",r.DdM(15,G)),r.xp6(5),r.Akn(r.DdM(16,G)),r.Q6J("disabledDays",r.DdM(17,H))("inputStyle",r.DdM(18,G)))},directives:[s.yS,a._Y,a.JL,a.sg,a.EJ,a.JJ,a.u,n.sg,Z.f,a.YN,a.Kr],styles:[".p-calendar .p-inputtext{flex:0 0 auto;width:100%;margin-top:-11px;margin-left:-16px;margin-right:-16px}"]}),e})();var O=i(5525),P=i(4999);function L(e,t){if(1&e&&(r.TgZ(0,"option",45),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function B(e,t){if(1&e&&(r.TgZ(0,"option",45),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function $(e,t){if(1&e&&(r.TgZ(0,"option",45),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function z(e,t){if(1&e&&(r.TgZ(0,"option",45),r._uU(1),r.qZA()),2&e){const e=t.$implicit;r.Q6J("value",e.id),r.xp6(1),r.hij(" ",e.name," ")}}function X(e,t){1&e&&r._UZ(0,"span")}function K(e,t){if(1&e){const e=r.EpF();r.TgZ(0,"span"),r.TgZ(1,"button",46),r.NdJ("click",function(){const t=r.CHM(e),i=t.row,n=t.rowIndex;return r.oxw().editExsitingAttendance(i,n)}),r._UZ(2,"i",47),r.qZA(),r.TgZ(3,"button",48),r.NdJ("click",function(){const t=r.CHM(e).row;return r.oxw().deleteSelecedAttendance(t)}),r._UZ(4,"i",49),r.qZA(),r.qZA()}}function W(e,t){if(1&e&&(r.TgZ(0,"div",50),r.TgZ(1,"div"),r._uU(2),r.qZA(),r.qZA()),2&e){const e=t.row;r.xp6(2),r.Oqu(e.className)}}function ee(e,t){1&e&&r._uU(0),2&e&&r.hij(" ",t.row.subjectName," ")}function te(e,t){1&e&&r._uU(0),2&e&&r.hij(" ",t.row.date," ")}function ie(e,t){1&e&&r._uU(0),2&e&&r.hij(" ",t.row.startTime," ")}function ne(e,t){1&e&&r._uU(0),2&e&&r.hij(" ",t.row.endTime," ")}function se(e,t){1&e&&r._uU(0),2&e&&r.hij(" ",t.row.totalAttendedStudents," ")}function ae(e,t){1&e&&r._uU(0),2&e&&r.hij(" ",t.row.totalAbsenceStudents," ")}const re=[{path:"",redirectTo:"attendance-list",pathMatch:"full"},{path:"attendance-list",component:(()=>{class e{constructor(e,t,i,n,s,a){this.attendanceService=e,this.formBuilder=t,this.modalService=i,this.router=n,this.dropdownService=s,this.spinner=a,this.rows=[],this.newUserImg="assets/images/users/user-2.png",this.scrollBarHorizontal=window.innerWidth<1200,this.loadingIndicator=!0,this.reorderable=!0,this.academicYears=[],this.grades=[],this.classes=[],this.subjects=[],this.data=new Array,this.currentPage=0,this.pageSize=25,this.totalRecord=0}ngOnInit(){this.filterForm=this.createFilterForm(),this.spinner.show(),this.loadMasterData()}createFilterForm(){return new a.cw({selectedAcademicYearId:new a.NI(0),selectedGradeId:new a.NI(0),selectedClassId:new a.NI(0),selectedSubjectId:new a.NI(0),searchText:new a.NI("")})}onAcademicYearFilterChanged(e){this.spinner.show(),this.resetPaging(),this.loadClassesForTecher()}onGradeFilterChanged(e){this.spinner.show(),this.resetPaging(),this.loadClassesForTecher()}onClassFilterChanged(e){this.spinner.show(),this.resetPaging(),this.loadSubjectForTeacher()}onSubjectFilterChanged(e){this.spinner.show(),this.resetPaging(),this.loadAttendance()}filterDatatable(e){e.target.value.toLowerCase(),this.resetPaging(),this.spinner.show(),this.loadAttendance()}setPage(e){this.spinner.show(),this.currentPage=e.offset,this.loadAttendance()}loadMasterData(){this.attendanceService.getAttendanceListTeacherDropdownMasterData().subscribe(e=>{this.academicYears=e.academicYears,this.grades=e.grades,this.classes=e.classes,this.subjects=e.subjects,this.filterForm.get("selectedAcademicYearId").setValue(e.currentAcademicYear),this.filterForm.get("selectedGradeId").setValue(e.selectedGradeId),this.filterForm.get("selectedClassId").setValue(e.selectedClassId),this.filterForm.get("selectedSubjectId").setValue(e.selectedSubjectId),this.loadAttendance()},e=>{})}loadClassesForTecher(){this.dropdownService.getAssignedClassForLoggedInUser(this.selectedAcademicYearId,this.selectedGradeId).subscribe(e=>{this.classes=e;let t=new P.u;t.id=0,t.name="--All--",this.classes.unshift(t),this.filterForm.get("selectedClassId").setValue(0),this.loadSubjectForTeacher()},e=>{this.spinner.hide()})}loadSubjectForTeacher(){this.dropdownService.getAssignedClassSubjectForLoggedInUser(this.selectedClassId).subscribe(e=>{this.subjects=e;let t=new P.u;t.id=0,t.name="--All--",this.subjects.unshift(t),this.filterForm.get("selectedSubjectId").setValue(0),this.loadAttendance()},e=>{this.spinner.hide()})}loadAttendance(){this.attendanceService.getMySubjectAttendance(this.searchText,this.currentPage+1,this.pageSize,this.selectedAcademicYearId,this.selectedGradeId,this.selectedClassId,this.selectedSubjectId).subscribe(e=>{this.data=e.data,this.totalRecord=e.totalRecordCount,this.loadingIndicator=!1,this.spinner.hide()},e=>{this.spinner.hide()})}resetPaging(){this.currentPage=0,this.totalRecord=0}addNewAttendance(){this.router.navigate(["/attendance/attendance-list",0])}editExsitingAttendance(e,t){this.router.navigate(["/attendance/attendance-list",e.id])}deleteSelecedAttendance(e){}get selectedAcademicYearId(){return this.filterForm.get("selectedAcademicYearId").value}get selectedGradeId(){return this.filterForm.get("selectedGradeId").value}get selectedClassId(){return this.filterForm.get("selectedClassId").value}get selectedSubjectId(){return this.filterForm.get("selectedSubjectId").value}get searchText(){return this.filterForm.get("searchText").value}}return e.\u0275fac=function(t){return new(t||e)(r.Y36(h),r.Y36(a.qu),r.Y36(u.FF),r.Y36(s.F0),r.Y36(m.V),r.Y36(p.t2))},e.\u0275cmp=r.Xpm({type:e,selectors:[["app-subject-attendance-list"]],viewQuery:function(e,t){if(1&e&&r.Gf(O.nE,5),2&e){let e;r.iGM(e=r.CRH())&&(t.table=e.first)}},decls:78,vars:24,consts:[[1,"main-content"],[1,"breadcrumb","breadcrumb-style"],[1,"breadcrumb-item"],[1,"page-title","m-b-0"],[1,"breadcrumb-item","bcrumb-1"],["routerLink","/attendance/attendance-list"],[1,"fas","fa-home","font-17"],[1,"section-body"],[1,"row","clearfix"],[1,"col-lg-12","col-md-12","col-sm-12","col-xs-12"],[1,"card"],[1,"p-10"],[1,"row"],[1,"row","m-0"],[3,"formGroup"],[1,"col-12","p-0"],[1,"ngxTableHeader"],[1,"table-title"],[1,"col-lg-12","col-md-12","col-sm-12","col-xs-12","p-0"],[1,"col-lg-3","col-md-3","col-sm-12","col-xs-12"],[1,"form-group","position-relative"],[1,"fas","fa-search","input-icons"],["type","text","formControlName","searchText","placeholder","Search by name","name","searchText","required","",1,"form-control","psl-5",3,"keyup"],[1,"col-lg-2","col-md-2","col-sm-12","col-xs-12"],[1,"form-group"],["formControlName","selectedAcademicYearId",1,"form-select","form-control",3,"change"],[3,"value",4,"ngFor","ngForOf"],["formControlName","selectedGradeId",1,"form-select","form-control",3,"change"],["formControlName","selectedClassId",1,"form-select","form-control",3,"change"],["formControlName","selectedSubjectId",1,"form-select","form-control",3,"change"],[1,"col-lg-2","col-md-2","col-sm-12","col-xs-12","ps-3","pe-3","pt-3"],["type","button",1,"btn","btn-primary","mb-2",2,"width","100%",3,"click"],["columnMode","force","rowHeight","auto",1,"material",3,"rows","loadingIndicator","headerHeight","footerHeight","externalPaging","count","offset","limit","scrollbarH","reorderable","page"],["table",""],["name","",3,"width","sortable"],["sortable","false","ngx-datatable-header-template","","class","text-center"],["ngx-datatable-cell-template","","class","text-center"],["name","Class",3,"width"],["ngx-datatable-cell-template",""],["name","Subject",3,"width"],["name","Date",3,"width"],["name","Start Start",3,"width"],["name","End Time",3,"width"],["name","Tot-Attended",3,"width"],["name","Tot-Absence",3,"width"],[3,"value"],[1,"btn","btn-icon","btn-sm","btn-primary","ms-1",3,"click"],[1,"fas","fa-pen"],[1,"btn","btn-icon","btn-sm","btn-danger","ms-1",3,"click"],[1,"fas","fa-trash-alt"],[1,"name-col-style"]],template:function(e,t){1&e&&(r.TgZ(0,"section",0),r.TgZ(1,"ul",1),r.TgZ(2,"li",2),r.TgZ(3,"h5",3),r._uU(4,"Attendance"),r.qZA(),r.qZA(),r.TgZ(5,"li",4),r.TgZ(6,"a",5),r._UZ(7,"i",6),r.qZA(),r.qZA(),r.qZA(),r.TgZ(8,"div",7),r.TgZ(9,"div",8),r.TgZ(10,"div",9),r.TgZ(11,"div",10),r.TgZ(12,"div",11),r.TgZ(13,"div",12),r.TgZ(14,"div",9),r.TgZ(15,"div",13),r.TgZ(16,"form",14),r.TgZ(17,"div",15),r.TgZ(18,"div",16),r.TgZ(19,"div",17),r.TgZ(20,"h2"),r.TgZ(21,"strong"),r._uU(22,"Subject Attendance"),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.TgZ(23,"div",18),r.TgZ(24,"div",12),r.TgZ(25,"div",19),r.TgZ(26,"div",20),r.TgZ(27,"label"),r._uU(28,"Search Text"),r.qZA(),r._UZ(29,"i",21),r.TgZ(30,"input",22),r.NdJ("keyup",function(e){return t.filterDatatable(e)}),r.qZA(),r.qZA(),r.qZA(),r.TgZ(31,"div",23),r.TgZ(32,"div",24),r.TgZ(33,"label"),r._uU(34,"Academic Year"),r.qZA(),r.TgZ(35,"select",25),r.NdJ("change",function(e){return t.onAcademicYearFilterChanged(e.target.value)}),r.YNc(36,L,2,2,"option",26),r.qZA(),r.qZA(),r.qZA(),r.TgZ(37,"div",23),r.TgZ(38,"div",24),r.TgZ(39,"label"),r._uU(40,"Grade"),r.qZA(),r.TgZ(41,"select",27),r.NdJ("change",function(e){return t.onGradeFilterChanged(e.target.value)}),r.YNc(42,B,2,2,"option",26),r.qZA(),r.qZA(),r.qZA(),r.TgZ(43,"div",23),r.TgZ(44,"div",24),r.TgZ(45,"label"),r._uU(46,"Class"),r.qZA(),r.TgZ(47,"select",28),r.NdJ("change",function(e){return t.onClassFilterChanged(e.target.value)}),r.YNc(48,$,2,2,"option",26),r.qZA(),r.qZA(),r.qZA(),r.TgZ(49,"div",23),r.TgZ(50,"div",24),r.TgZ(51,"label"),r._uU(52,"Subject"),r.qZA(),r.TgZ(53,"select",29),r.NdJ("change",function(e){return t.onSubjectFilterChanged(e.target.value)}),r.YNc(54,z,2,2,"option",26),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.TgZ(55,"div",12),r.TgZ(56,"div",30),r.TgZ(57,"button",31),r.NdJ("click",function(){return t.addNewAttendance()}),r._uU(58," Add New Attendance "),r.qZA(),r.qZA(),r.qZA(),r.TgZ(59,"ngx-datatable",32,33),r.NdJ("page",function(e){return t.setPage(e)}),r.TgZ(61,"ngx-datatable-column",34),r.YNc(62,X,1,0,"ng-template",35),r.YNc(63,K,5,0,"ng-template",36),r.qZA(),r.TgZ(64,"ngx-datatable-column",37),r.YNc(65,W,3,1,"ng-template",38),r.qZA(),r.TgZ(66,"ngx-datatable-column",39),r.YNc(67,ee,1,1,"ng-template",38),r.qZA(),r.TgZ(68,"ngx-datatable-column",40),r.YNc(69,te,1,1,"ng-template",38),r.qZA(),r.TgZ(70,"ngx-datatable-column",41),r.YNc(71,ie,1,1,"ng-template",38),r.qZA(),r.TgZ(72,"ngx-datatable-column",42),r.YNc(73,ne,1,1,"ng-template",38),r.qZA(),r.TgZ(74,"ngx-datatable-column",43),r.YNc(75,se,1,1,"ng-template",38),r.qZA(),r.TgZ(76,"ngx-datatable-column",44),r.YNc(77,ae,1,1,"ng-template",38),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA(),r.qZA()),2&e&&(r.xp6(16),r.Q6J("formGroup",t.filterForm),r.xp6(20),r.Q6J("ngForOf",t.academicYears),r.xp6(6),r.Q6J("ngForOf",t.grades),r.xp6(6),r.Q6J("ngForOf",t.classes),r.xp6(6),r.Q6J("ngForOf",t.subjects),r.xp6(5),r.Q6J("rows",t.data)("loadingIndicator",t.loadingIndicator)("headerHeight",60)("footerHeight",80)("externalPaging",!0)("count",t.totalRecord)("offset",t.currentPage)("limit",t.pageSize)("scrollbarH",t.scrollBarHorizontal)("reorderable",t.reorderable),r.xp6(2),r.Q6J("width",150)("sortable",!1),r.xp6(3),r.Q6J("width",150),r.xp6(2),r.Q6J("width",150),r.xp6(2),r.Q6J("width",150),r.xp6(2),r.Q6J("width",150),r.xp6(2),r.Q6J("width",150),r.xp6(2),r.Q6J("width",150),r.xp6(2),r.Q6J("width",150))},directives:[s.yS,a._Y,a.JL,a.sg,a.Fj,a.JJ,a.u,a.Q7,a.EJ,n.sg,O.nE,O.UC,O.tk,O.vq,a.YN,a.Kr],styles:[""]}),e})()},{path:"attendance-list/:id",component:J},{path:"reports",component:E}];let oe=(()=>{class e{}return e.\u0275fac=function(t){return new(t||e)},e.\u0275mod=r.oAB({type:e}),e.\u0275inj=r.cJS({imports:[[s.Bz.forChild(re)],s.Bz]}),e})();var le=i(4082),de=i(3492);let ce=(()=>{class e{}return e.\u0275fac=function(t){return new(t||e)},e.\u0275mod=r.oAB({type:e}),e.\u0275inj=r.cJS({imports:[[n.ez,le.q,a.u5,a.UX,Z._8,w,f.U$,O.xD,de.Rh.forRoot({positionClass:"toast-bottom-right"}),oe]]}),e})()}}]);