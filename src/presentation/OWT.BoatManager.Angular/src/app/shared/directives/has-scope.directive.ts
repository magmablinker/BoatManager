import { Directive, inject, Input, OnInit, TemplateRef, ViewContainerRef } from "@angular/core";
import { UserScopeService } from "../services/user-scope.service";

@Directive({
    selector: '[hasScope]'
  })
  export class HasScopeDirective implements OnInit {
    @Input('hasScope') scope!: string;

    private readonly templateRef = inject(TemplateRef<any>)
    private readonly viewContainer = inject(ViewContainerRef);
    private readonly userScopeService = inject(UserScopeService);

    async ngOnInit() {
      const hasAccess = await this.userScopeService.hasScope(this.scope);
      
      if (hasAccess) {
        this.viewContainer.createEmbeddedView(this.templateRef);
      } else {
        this.viewContainer.clear();
      }
    }
  }