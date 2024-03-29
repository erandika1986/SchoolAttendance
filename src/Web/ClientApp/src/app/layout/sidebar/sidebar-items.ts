import { RouteInfo } from './sidebar.metadata';
export const ROUTES: RouteInfo[] = [
  {
    path: '',
    title: 'MENUITEMS.MAIN.TEXT',
    iconType: '',
    icon: '',
    class: '',
    groupTitle: true,
    badge: '',
    badgeClass: '',
    submenu: [],
    isVisible: false,
  },
  {
    path: '',
    title: 'MENUITEMS.HOME.TEXT',
    iconType: 'feather',
    icon: 'monitor',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible: false,
    submenu: [
      {
        path: '/admin/teachers',
        title: 'MENUITEMS.HOME.LIST.TEACHERS',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/admin/students',
        title: 'MENUITEMS.HOME.LIST.STUDENTS',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/admin/subject',
        title: 'MENUITEMS.HOME.LIST.SUBJECT',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/admin/grades',
        title: 'MENUITEMS.HOME.LIST.GRADES',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/admin/class',
        title: 'MENUITEMS.HOME.LIST.CLASSES',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/admin/excel-upload',
        title: 'MENUITEMS.HOME.LIST.EXCEL_UPLOAD',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
    ],
  },
  {
    path: '',
    title: 'MENUITEMS.ATTENDANCE.TEXT',
    iconType: 'feather',
    icon: 'monitor',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible: false,
    submenu: [
      {
        path: '/attendance/attendance-list',
        title: 'MENUITEMS.ATTENDANCE.LIST.ATTENDANCE_LIST',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/attendance/reports',
        title: 'MENUITEMS.ATTENDANCE.LIST.ATTENDANCE_REPORT',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      }
    ],
  },
  {
    path: '',
    title: 'MENUITEMS.ASSESSMENT.TEXT',
    iconType: 'feather',
    icon: 'monitor',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible: false,
    submenu: [
      {
        path: '/assessment/assessment-list',
        title: 'MENUITEMS.ASSESSMENT.LIST.ASSESSMENT_LIST',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/attendance/reports',
        title: 'MENUITEMS.ASSESSMENT.LIST.ASSESSMENT_RESULTS',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      }
    ],
  },
  {
    path: '',
    title: 'MENUITEMS.LESSON.TEXT',
    iconType: 'feather',
    icon: 'monitor',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible: false,
    submenu: [
      {
        path: '/teacher-lessons/lessons-in-design',
        title: 'MENUITEMS.LESSON.LIST.MY_LESSON_LIST',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      }
    ],
  },
  {
    path: 'advance-table',
    title: 'MENUITEMS.ADVANCE-TABLE.TEXT',
    iconType: 'feather',
    icon: 'trello',
    class: '',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    submenu: [],
    isVisible: false,
  },
  {
    path: '',
    title: '-- Pages',
    iconType: '',
    icon: '',
    class: '',
    groupTitle: true,
    badge: '',
    badgeClass: '',
    submenu: [],
    isVisible: false,
  },
  {
    path: '',
    title: 'Authentication',
    iconType: 'feather',
    icon: 'user-check',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible: false,
    submenu: [
      {
        path: '/authentication/signin',
        title: 'Sign In',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/authentication/signup',
        title: 'Sign Up',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/authentication/forgot',
        title: 'Forgot Password',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/authentication/reset',
        title: 'Reset Password',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/authentication/page404',
        title: '404 - Not Found',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/authentication/page500',
        title: '500 - Server Error',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
    ],
  },
  {
    path: '',
    title: 'Extra Pages',
    iconType: 'feather',
    icon: 'anchor',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible: false,
    submenu: [
      {
        path: '/extra-pages/blank',
        title: 'Blank Page',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
    ],
  },
  {
    path: '',
    title: 'Multi level Menu',
    iconType: 'feather',
    icon: 'chevrons-down',
    class: 'menu-toggle',
    groupTitle: false,
    badge: '',
    badgeClass: '',
    isVisible: false,
    submenu: [
      {
        path: '/multilevel/first1',
        title: 'First',
        iconType: '',
        icon: '',
        class: 'ml-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
      {
        path: '/',
        title: 'Second',
        iconType: '',
        icon: '',
        class: 'ml-sub-menu',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        isVisible: false,
        submenu: [
          {
            path: '/multilevel/secondlevel/second1',
            title: 'Second 1',
            iconType: '',
            icon: '',
            class: 'ml-menu2',
            groupTitle: false,
            badge: '',
            badgeClass: '',
            submenu: [],
            isVisible: false,
          },
          {
            path: '/',
            title: 'Second 2',
            iconType: '',
            icon: '',
            class: 'ml-sub-menu2',
            groupTitle: false,
            badge: '',
            badgeClass: '',
            isVisible: false,
            submenu: [
              {
                path: '/multilevel/thirdlevel/third1',
                title: 'third 1',
                iconType: '',
                icon: '',
                class: 'ml-menu3',
                groupTitle: false,
                badge: '',
                badgeClass: '',
                submenu: [],
                isVisible: false,
              },
            ],
          },
        ],
      },
      {
        path: '/multilevel/first3',
        title: 'Third',
        iconType: '',
        icon: '',
        class: 'ml-menu2',
        groupTitle: false,
        badge: '',
        badgeClass: '',
        submenu: [],
        isVisible: false,
      },
    ],
  },
];
