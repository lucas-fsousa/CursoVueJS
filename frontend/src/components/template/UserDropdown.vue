<template>
  <div class="user-dropdown">
    <div class="user-button">
      <span class="d-none d-sm-block">{{ user.name }}</span>
      <div class="user-dropdown-img">
        <img src="@/assets/imgs/user-img.jpg" alt="user-image" />
      </div>
      <font-awesome-icon icon="fa-solid fa-arrow-down" />
    </div>
    <div class="user-dropdown-content">
      <router-link to="/admin"
        ><font-awesome-icon icon="fa-solid fa-cogs" />
        Administração</router-link
      >
      <router-link to="/"
        ><font-awesome-icon icon="fa-solid fa-sign-out" /> Sair</router-link
      >
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  computed: {
    ...mapState(["user"]),
  },
};
</script>

<style>
.user-dropdown {
  position: relative;
  height: 100%;
}
.user-dropdown-content {
  position: absolute;
  right: 0px;
  background-color: #f9f9f9;
  min-width: 170px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  padding: 10px;
  z-index: 1;

  display: flex;
  flex-direction: column;
  flex-wrap: wrap;

  visibility: hidden;
  opacity: 0;
  transition: visibility 0s, opacity 0.5s linear;
}

.user-dropdown:hover .user-dropdown-content {
  visibility: visible;
  opacity: 1;
}

.user-button {
  display: flex;
  align-items: center;
  color: #fff;
  font-weight: 100;
  height: 100%;
  padding: 0px 20px;
}

.user-button:hover {
  background-color: rgba(0, 0, 0, 0.3);
}

.user-dropdown-img {
  margin: 0px 10px;
}

.user-dropdown-img > img {
  border-radius: 25px;
  max-height: 37px;
}

.user-dropdown-content a {
  text-decoration: none;
  color: #000;
  padding: 5px;
}

.user-dropdown-content a:hover {
  background-color: #ededed;
  border-radius: 5px;
}
</style>
