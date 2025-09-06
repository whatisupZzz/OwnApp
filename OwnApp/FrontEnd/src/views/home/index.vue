<template>
  <div class="home-container">
    <header class="header">
      <div class="logo">Vue3 App</div>
      <div class="user-info">
        <span>{{ userInfo.nickname || userInfo.username }}</span>
        <button @click="handleLogout">退出登录</button>
      </div>
    </header>
    <main class="main">
      <h1>欢迎使用 Vue3 + TypeScript 应用</h1>
      <p>您已成功登录系统</p>
    </main>
  </div>
</template>

<script lang="ts" setup>
import { useRouter } from 'vue-router';
import { useUserStore } from '@/store/modules/user';
import { computed } from 'vue';

const router = useRouter();
const userStore = useUserStore();

// 获取用户信息
const userInfo = computed(() => userStore.getUserInfo);

// 处理登出
const handleLogout = () => {
  userStore.logout();
  router.push('/login');
};
</script>

<style lang="scss" scoped>
.home-container {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  
  .header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 20px;
    height: 60px;
    background-color: #1890ff;
    color: #fff;
    
    .logo {
      font-size: 20px;
      font-weight: bold;
    }
    
    .user-info {
      display: flex;
      align-items: center;
      
      span {
        margin-right: 15px;
      }
      
      button {
        padding: 5px 15px;
        background-color: rgba(255, 255, 255, 0.2);
        color: #fff;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        transition: background-color 0.3s;
        
        &:hover {
          background-color: rgba(255, 255, 255, 0.3);
        }
      }
    }
  }
  
  .main {
    flex: 1;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background-color: #f5f5f5;
    
    h1 {
      font-size: 28px;
      color: #333;
      margin-bottom: 20px;
    }
    
    p {
      font-size: 16px;
      color: #666;
    }
  }
}
</style>