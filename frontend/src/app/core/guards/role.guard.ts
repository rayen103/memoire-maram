import { inject } from '@angular/core';
import { CanActivateFn, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const roles = (route.data['roles'] as string[]) ?? [];

  if (authService.hasRole(roles)) {
    return true;
  }

  return router.createUrlTree(['/auth/login']);
};

export const RoleGuard = roleGuard;
