import { Router, NavigationEnd } from '@angular/router';
import { DOCUMENT } from '@angular/common';
import {
  Component,
  Inject,
  ElementRef,
  OnInit,
  Renderer2,
  HostListener,
  OnDestroy,
} from '@angular/core';
import { ROUTES } from './sidebar-items';
import { AuthService } from 'src/app/core/services/auth.service';
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.sass'],
})
export class SidebarComponent implements OnInit, OnDestroy {
  public sidebarItems: any[];
  public innerHeight: any;
  public bodyTag: any;
  listMaxHeight: string;
  listMaxWidth: string;
  userFullName: string;
  userImg: string;
  userType: string;
  headerHeight = 60;
  routerObj;
  currentRoute: string;
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private renderer: Renderer2,
    public elementRef: ElementRef,
    public authService: AuthService,
    private router: Router
  ) {
    this.routerObj = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        // close sidebar on mobile screen after menu select
        this.renderer.removeClass(this.document.body, 'overlay-open');
        this.sidebbarClose();
      }
    });
  }
  @HostListener('window:resize', ['$event'])
  windowResizecall(event:any) {
    if (window.innerWidth < 1025) {
      this.renderer.removeClass(this.document.body, 'side-closed');
    }
    this.setMenuHeight();
    this.checkStatuForResize(false);
  }
  @HostListener('document:mousedown', ['$event'])
  onGlobalClick(event:any): void {
    if (!this.elementRef.nativeElement.contains(event.target)) {
      this.renderer.removeClass(this.document.body, 'overlay-open');
      this.sidebbarClose();
    }
  }
  callToggleMenu(event: any, length: any) {
    if (length > 0) {
      const parentElement = event.target.closest('li');
      const activeClass = parentElement.classList.contains('active');

      if (activeClass) {
        this.renderer.removeClass(parentElement, 'active');
      } else {
        this.renderer.addClass(parentElement, 'active');
      }
    }
  }
  ngOnInit() {

    console.log("XXXXXXXXXXXXXXXXXXXXX");
    console.log(this.authService.currentUserValue);
    
    if (this.authService.currentUserValue) {
      this.sidebarItems = ROUTES.filter((sidebarItem) => 
      {
        console.log(sidebarItem.title)
        if(sidebarItem.title === "MENUITEMS.HOME.TEXT")
        {
          sidebarItem.isVisible = this.isUserRoleExists('Admin');
          for (const item of sidebarItem.submenu) {
            item.isVisible = true;
					}
        }
        else if(sidebarItem.title === "MENUITEMS.ATTENDANCE.TEXT")
        {
          sidebarItem.isVisible = this.isUserRoleExists('Admin') ||  this.isUserRoleExists('LevelHead') ||  this.isUserRoleExists('DepartmentHead')||  this.isUserRoleExists('Teacher');
          for (const item of sidebarItem.submenu) {
            item.isVisible = true;
					}
        }
        else if(sidebarItem.title === "MENUITEMS.ASSESSMENT.TEXT")
        {
          sidebarItem.isVisible = this.isUserRoleExists('Admin') ||  this.isUserRoleExists('Principle') ||  this.isUserRoleExists('VicePrinciple') ||  this.isUserRoleExists('LevelHead') ||  this.isUserRoleExists('DepartmentHead')||  this.isUserRoleExists('Teacher');
          for (const item of sidebarItem.submenu) {
            item.isVisible = true;
					}
        }
        return sidebarItem;
      });
    }
    this.initLeftSidebar();
    this.bodyTag = this.document.body;
  }

  isUserRoleExists(role: string): boolean {
		for (let index = 0; index < this.authService.currentUserValue.roles.length; index++) {
			if (this.authService.currentUserValue.roles[index] == role) {
				return true;
			}
		}

		return false;
	}

  ngOnDestroy() {
    this.routerObj.unsubscribe();
  }
  initLeftSidebar() {
    const _this = this;
    // Set menu height
    _this.setMenuHeight();
    _this.checkStatuForResize(true);
  }
  setMenuHeight() {
    this.innerHeight = window.innerHeight;
    const height = this.innerHeight - this.headerHeight;
    this.listMaxHeight = height + '';
    this.listMaxWidth = '500px';
  }
  isOpen() {
    return this.bodyTag.classList.contains('overlay-open');
  }
  checkStatuForResize(firstTime:any) {
    if (window.innerWidth < 1025) {
      this.renderer.addClass(this.document.body, 'sidebar-gone');
    } else {
      this.renderer.removeClass(this.document.body, 'sidebar-gone');
    }
  }
  mouseHover(e:any) {
    const body = this.elementRef.nativeElement.closest('body');
    if (body.classList.contains('submenu-closed')) {
      this.renderer.addClass(this.document.body, 'side-closed-hover');
      this.renderer.removeClass(this.document.body, 'submenu-closed');
    }
  }
  mouseOut(e:any) {
    const body = this.elementRef.nativeElement.closest('body');
    if (body.classList.contains('side-closed-hover')) {
      this.renderer.removeClass(this.document.body, 'side-closed-hover');
      this.renderer.addClass(this.document.body, 'submenu-closed');
    }
  }

  sidebbarClose() {
    if (window.innerWidth < 1025) {
      this.renderer.addClass(this.document.body, 'sidebar-gone');
    }
  }
}
