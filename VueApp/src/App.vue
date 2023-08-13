<template>
  <div>
    <h2>Таблица-дерево</h2>
    <input type="text" placeholder="Поиск по ID" v-model="itemToFetch">
    <button @click="fetchItemById(itemToFetch)">Получить данные</button>
    <tree-grid :items="item" :columns="treeColumns"></tree-grid>
    <h2>Плоский список</h2>
    <button @click="fetchItems()">Получить данные</button>
    <flat-list 
      :items="items" 
      :columns="flatListColumns"
      @submit="addItem"
    />
  </div>
</template>

<script>
import TreeGrid from "@/components/TreeGrid.vue";
import FlatList from "@/components/FlatList.vue";
import axios from 'axios';
export default {
  components: {
    TreeGrid,
    FlatList
  },
  data() {
    return {
      treeColumns: [
        { field: 'id' },
        { field: 'продукт' },
        { field: 'цена' },
        { field: 'количество' },
        {field: 'стоимость'}
      ],
      flatListColumns : [
        { field: 'id' },
        { field: 'продукт' },
        { field: 'цена' },
      ],
      item: [],
      items: [],
    };
  },
  methods: {
    async addItem(item) {
      console.log(item)
      try {
        const response = await axios.post('http://localhost:5164/api/nomenklatures', item);
        console.log(response);
        this.items.push(item);
      } catch (error) {
        alert('Ошибка')
      }
    },
    async fetchItems() {
      try {
        const response = await axios.get('http://localhost:5164/api/nomenklatures');
        this.items = response.data;
      } catch (error) {
        alert('Ошибка')
      }
    },
    async fetchItemById(id) {
      if (id)
        try {
          const response = await axios.get('http://localhost:5164/api/nomenklatures/' + id);
          console.log(response);
          this.item = response.data;
          console.log(this.item);
        } catch (error) {
          alert('Ошибка')
        }
    }
  }
};
</script>


<style scoped>

</style>
