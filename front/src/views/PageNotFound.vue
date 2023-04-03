<template>
    <div class="container flex flex-col items-center mt-10">
        <h1 class="text-3xl mb-3">
            <img src="https://media3.giphy.com/media/AQpUsaKCRD9gA/giphy.gif?cid=790b761114fd781bae8a6def7cfba744eb4db5745e3c7680&rid=giphy.gif&ct=g" alt="this slowpoke moves"  width="262" />
            <br>
            Sorry, this page does not exist!</h1>
        <div class="wrapper">  
            <router-link :to="{ name: 'Calendar'}"> 
                Back To Home
            </router-link>             
        </div>
    </div>
</template>

<script>
export default {};
</script>

<style scoped>
h1{
  margin-top: 9%;
    top: 30%;
    left: 50%;
}

.wrapper{
  top: 63%;
  margin-left: 40%;
}

a{
  display: block;
  width: 250px;
  height: 50px;
  line-height: 50px;
  font-weight: bold;
  text-decoration: none;
  background: #3f3229;
  text-align: center;
  color: #fff;
  text-transform: uppercase;
  letter-spacing: 1px;
  border: 3px solid #3f3229;
  transition: all .35s;
  margin-top: 3%;
  margin-left: 1%;
}

a:hover{
  width: 200px;
  border: 3px solid #3f3229;
  background: transparent;
  color: #3f3229;
  margin-left: 5%;
}

</style>