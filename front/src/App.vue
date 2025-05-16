<script setup lang="ts">
import { RouterView } from 'vue-router';

const links = [
  { name: 'Carte', path: '/' },
];
</script>

<template>
  <header>
    <nav class="bg-gray-200 p-4">
      <ul class="flex gap-4">
        <li v-for="(link, index) in links" :key="index">
          <RouterLink :to="link.path" :exact-active-class="'font-medium'">
            {{ link.name }}
          </RouterLink>
        </li>
      </ul>
    </nav>
  </header>
  <main class="p-4">
    <RouterView />
  </main>
</template>
