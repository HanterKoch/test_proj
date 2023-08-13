<template>
  <tr class="header">
    <td v-for="column in columns">{{ column.field }}</td>
  </tr>
  <div v-for="item in items">
    <tr>
      <td>{{ item.id }}</td>
      <td>
        <span v-if="item.children.length != 0" @click="isExpanded(item)">{{ setFlagSign(item) }}</span>
        {{ item.name }}
      </td>
      <td>{{ item.price }}</td>
      <td>{{ item.quantity }}</td>
      <td>{{ calculateTotalPrice(item) }}</td>
    </tr>
    <tree-grid v-if="item.expanded && item.children" :items="item.children"></tree-grid>
  </div>
</template>

<script>
export default {
  name: "tree-grid",
  props: {
    items: {
      type: Array,
      required: true
    },
    columns: {
      type: Array,
      required: false
    }
  },
  methods: {
    calculateTotalPrice(item) {
      let totalPrice = item.price;
      if (item.children) {
        item.children.forEach(child => {
          totalPrice += this.calculateTotalPrice(child);
        });
      }
      return totalPrice * item.quantity;
    },
    setFlagSign(item) {
      return item.expanded ? '˅' : '>';
    },
    isExpanded(item) {
      item.expanded = !item.expanded;
    }
  }
};
</script>

<style>
 td {
  border: 1px solid;
  min-width: 200px;
 }
</style>