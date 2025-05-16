<template>
  <div class="flex flex-col gap-4">
    <AppCard>
      <div class="prose max-w-none">
        <p>The website should contain a form that allows to give the properties of a building:</p>
        <ul>
          <li>ground area</li>
          <li>number of levels</li>
          <li>usage</li>
        </ul>
        <p>These informations are sent to the back that uses a proprietary algoritm to calculate its consumption.</p>
      </div>
    </AppCard>
    <div class="grid grid-cols-2 gap-4">
      <AppCard>
        <h2 class="font-medium">Formulaire</h2>
        <!-- Your code -->
      </AppCard>
      <AppCard>
        <h2 class="font-medium">Résultat</h2>
        <!-- Your code -->
      </AppCard>
    </div>
  </div>
</template>

<script setup lang="ts">
import AppCard from '@/components/AppCard.vue';
</script>
