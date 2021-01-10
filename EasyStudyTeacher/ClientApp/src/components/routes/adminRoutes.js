import React from 'react';



const Categories = React.lazy(() => import("../components/category/categories"));
const ProductCreate = React.lazy(() => import("../components/product/create"));

const adminRoutes =[
    { path: '/admin/categories', exact: true, name: 'Категорії', component: Categories  },
    { path: '/admin/product/create', exact: true, name: 'Cтворити продукт', component: ProductCreate  }
];
export default adminRoutes;