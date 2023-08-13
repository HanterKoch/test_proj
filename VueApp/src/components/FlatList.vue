<template>
    <div class="list">
        <tr class="header">
            <td v-for="column in columns">{{ column.field }}</td>
        </tr>

        <tr class="item" v-for="item in items" :key="item.id">
            <td v-for="field in item">{{ field }}</td>
        </tr>

        <button @click="addItemOption">{{ showInputs ? 'Отмена' : 'Добавить' }}</button>

        <div class="add-form" v-if="showInputs" @submit.prevent>
            <input 
                v-model="item.name" 
                @input="item.name = $event.target.value" 
                type="text" 
                placeholder="name">
            <input 
                v-model="item.price" 
                @input="item.price = $event.target.value" 
                type="text" 
                placeholder="price"
            >
            <input type="text">
        </div>

        <button @click="save(this.item)">Сохранить</button>
    </div>
</template>

<script>
export default {
    name: "flat-list",
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
    data() {
        return {
            item: {
                id: '',
                name: '',
                price: ''
            },
            showInputs: false
        }
    },
    methods: {
        addItemOption() {
            this.showInputs = !this.showInputs;
        },
        submit(item) {
            if (this.item.name != '' && this.item.price != '') {
                this.item.id = item.length + 1;
                this.$emit('submit', this.item);
                this.item = {
                    id : '',
                    name: '',
                    price: ''
                }
                this.showInputs = false;
            }
        }
    }
}
</script>

<style>
.list {
    margin-top: 20px;
}
td {
    border: 1px solid;
    height: 30px;
}
.cell {
    min-width: 50px;
}
</style>