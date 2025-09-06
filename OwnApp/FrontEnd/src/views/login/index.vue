<template>
  <div class="login-container">
    <div class="login-box">
      <div class="login-header">
        <h2>系统登录</h2>
      </div>
      <div class="login-form">
        <form @submit.prevent="handleLogin">
          <div class="form-item">
            <label for="username">用户名</label>
            <input 
              type="text" 
              id="username" 
              v-model="loginForm.username" 
              placeholder="请输入用户名" 
              required
            />
          </div>
          <div class="form-item">
            <label for="password">密码</label>
            <input 
              type="password" 
              id="password" 
              v-model="loginForm.password" 
              placeholder="请输入密码" 
              required
            />
          </div>
          <div class="form-item remember">
            <input 
              type="checkbox" 
              id="remember" 
              v-model="loginForm.remember"
            />
            <label for="remember">记住我</label>
          </div>
          <div class="form-item">
            <button type="submit" :disabled="loading">{{ loading ? '登录中...' : '登录' }}</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue';
import { useRouter } from 'vue-router';
import { useUserStore } from '@/store/modules/user';
import type { LoginParams } from '@/types/user';

const router = useRouter();
const userStore = useUserStore();
const loading = ref(false);

// 登录表单数据
const loginForm = reactive<LoginParams>({
  username: '',
  password: '',
  remember: false
});

// 处理登录
const handleLogin = async () => {
  if (!loginForm.username || !loginForm.password) {
    alert('请输入用户名和密码');
    return;
  }
  
  try {
    loading.value = true;
    await userStore.login(loginForm);
    router.push('/home');
  } catch (error: any) {
    alert(error.message || '登录失败，请重试');
  } finally {
    loading.value = false;
  }
};
</script>

<style lang="scss" scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f5f5f5;
  
  .login-box {
    width: 400px;
    padding: 30px;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 2px 12px rgba(0, 0, 0, 0.1);
    
    .login-header {
      text-align: center;
      margin-bottom: 30px;
      
      h2 {
        color: #333;
        font-size: 24px;
        font-weight: 500;
      }
    }
    
    .login-form {
      .form-item {
        margin-bottom: 20px;
        
        label {
          display: block;
          margin-bottom: 8px;
          color: #333;
          font-size: 14px;
        }
        
        input[type="text"],
        input[type="password"] {
          width: 100%;
          padding: 10px;
          border: 1px solid #ddd;
          border-radius: 4px;
          font-size: 14px;
          transition: border-color 0.3s;
          
          &:focus {
            border-color: #1890ff;
            outline: none;
          }
        }
        
        button {
          width: 100%;
          padding: 12px;
          background-color: #1890ff;
          color: #fff;
          border: none;
          border-radius: 4px;
          font-size: 16px;
          cursor: pointer;
          transition: background-color 0.3s;
          
          &:hover {
            background-color: #40a9ff;
          }
          
          &:disabled {
            background-color: #bae7ff;
            cursor: not-allowed;
          }
        }
        
        &.remember {
          display: flex;
          align-items: center;
          
          input {
            margin-right: 8px;
          }
          
          label {
            margin-bottom: 0;
          }
        }
      }
    }
  }
}
</style>