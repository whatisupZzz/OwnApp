<template>
  <div class="not-found">
    <div class="content">
      <h1>404</h1>
      <h2>页面不存在</h2>
      <p>抱歉，您访问的页面不存在或已被删除</p>
      <button @click="goHome">返回首页</button>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useRouter } from 'vue-router';

const router = useRouter();

const goHome = () => {
  router.push('/');
};
</script>

<style lang="scss" scoped>
.not-found {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f5f5f5;
  
  .content {
    text-align: center;
    
    h1 {
      font-size: 120px;
      margin: 0;
      color: #1890ff;
    }
    
    h2 {
      font-size: 30px;
      margin: 0;
      color: #333;
    }
    
    p {
      margin: 20px 0 30px;
      color: #666;
    }
    
    button {
      padding: 10px 20px;
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
    }
  }
}
</style>