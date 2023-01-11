<template>
  <router-view v-slot="{ Component }">
    <Transition
      :name="name"
      :mode="mode"
      :leave-to-class="leaveTo"
      :enter-to-class="enterTo"
      :leave-from-class="leaveFrom"
      :enter-from-class="enterFrom"
      :leave-active-class="leaveActive"
      :enter-active-class="enterActive"
    >
      <Component :is="Component" />
    </Transition>
  </router-view>
</template>

<script setup>
defineProps({
  enterFrom: String,
  leaveFrom: String,
  enterActive: String,
  leaveActive: String,
  enterTo: String,
  leaveTo: String,
  mode: String,
  name: String,
});
</script>

<style></style>
