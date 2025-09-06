import { createRouter, createWebHistory } from 'vue-router';
import type { RouteRecordRaw } from 'vue-router';

// 路由配置
const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/login'
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../views/login/index.vue'),
    meta: {
      title: '登录',
      requiresAuth: false
    }
  },
  {
    path: '/home',
    name: 'Home',
    component: () => import('../views/home/index.vue'),
    meta: {
      title: '首页',
      requiresAuth: true
    }
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: () => import('../views/error/404.vue'),
    meta: {
      title: '404',
      requiresAuth: false
    }
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

// 全局前置守卫
router.beforeEach((to, from, next) => {
  // 设置页面标题
  document.title = to.meta.title as string || 'Vue3 App';
  
  // 判断是否需要登录权限
  if (to.meta.requiresAuth) {
    // 从localStorage获取token
    const token = localStorage.getItem('token');
    if (token) {
      next();
    } else {
      // 未登录，重定向到登录页
      next({ name: 'Login' });
    }
  } else {
    next();
  }
});

export default router;