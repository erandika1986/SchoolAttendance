import { RouteInfo } from './sidebar.metadata';
export const ROUTES: RouteInfo[] = [
  {
    path: '',
    title: 'MENUITEMS.MAIN.TEXT',
    moduleName: '',
    iconType: '',
    icon: '',
    class: '',
    groupTitle: true,
    badge: '',
    badgeClass: '',
    submenu: [],
    isVisible:true
  },
  {
    path: '',
    title: 'MENUITEMS.TEACHER_LESSON.TEXT',
    moduleName: 'teacher-lesson',
    iconType: 'feather',
    icon: 'calendar',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible:true,
    submenu: [
      {
        path: '/teacher-lessons/lessons-in-design',
        title: 'MENUITEMS.TEACHER_LESSON.LIST.LESSON_IN_DESIGN',
        moduleName: 'teacher-lesson',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible:true
      }
    ],
  },
  {
    path: '',
    title: 'MENUITEMS.ATTENDANCE.TEXT',
    moduleName: 'attendance',
    iconType: 'feather',
    icon: 'calendar',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible:true,
    submenu: [
      {
        path: '/attendance/attendance-list',
        title: 'MENUITEMS.ATTENDANCE.LIST.MYATTENDANCE',
        moduleName: 'attendance',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible:true
      },
      {
        path: '/attendance/reports',
        title: 'MENUITEMS.ATTENDANCE.LIST.MYREPORT',
        moduleName: 'attendance',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible:true
      }
    ],
  },
  {
    path: '',
    title: 'MENUITEMS.ADMIN.TEXT',
    moduleName: 'admin',
    iconType: 'feather',
    icon: 'settings',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible:true,
    submenu: [
      {
        path: '/admin/teachers',
        title: 'MENUITEMS.ADMIN.LIST.TEACHERS',
        moduleName: 'admin',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible:true
      },
      {
        path: '/admin/students',
        title: 'MENUITEMS.ADMIN.LIST.STUDENTS',
        moduleName: 'admin',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible:true
      },
      {
        path: '/admin/subject',
        title: 'MENUITEMS.ADMIN.LIST.SUBJECT',
        moduleName: 'admin',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible:true
      },
      {
        path: '/admin/grades',
        title: 'MENUITEMS.ADMIN.LIST.GRADES',
        moduleName: 'admin',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible:true
      },
      {
        path: '/admin/class',
        title: 'MENUITEMS.ADMIN.LIST.CLASSES',
        moduleName: 'admin',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible:true
      },
      {
        path: '/admin/excel-upload',
        title: 'MENUITEMS.ADMIN.LIST.EXCEL_UPLOAD',
        moduleName: 'admin',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible:true
      },
    ],
  },

  
  


];
