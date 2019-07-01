import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  View,
} from 'react-native'
import styles from './styles'

export default class SiblingZOrder extends Component {
  static propTypes = {
    isFocusable: PropTypes.bool
  }

  render() {
    return (
      <View style={styles.content}>
        <Text selectable={this.props.isFocusable} accessibilityLabel={'Check issue https://github.com/facebook/react-native/issues/18344'} style={styles.subCaption}>Check issue https://github.com/facebook/react-native/issues/18344</Text>
        <Text selectable={this.props.isFocusable} accessibilityLabel={'zIndex only works when Views are siblings of each other'}>zIndex only works when Views are siblings of each other</Text>
        <View style={styles.testBar}>
          <Text selectable={this.props.isFocusable} accessibilityLabel={'Sibling no Z-Order'}>Sibling no Z-Order</Text>
          <View>
            <View styles={styles.item}>
              <View style={styles.redBox} />
              <View style={styles.greenBox} />
            </View>
          </View>
          <Text selectable={this.props.isFocusable} accessibilityLabel={'Sibling with Z-Order'}>    Sibling with Z-Order</Text>
          <View styles={styles.item}>
            <View style={[styles.redBox, { zIndex: 10 }]} />
            <View style={styles.greenBox} />
          </View>
          <Text selectable={this.props.isFocusable} accessibilityLabel={'NonSibling with Z-Order'}>   NonSibling with Z-Order</Text>
          <View styles={styles.item}>
            <View style={[styles.redBox, { zIndex: 10 }]} />
          </View>
        </View>
        <View id='box for NonSibling with Z-Order' style={[styles.greenBox, { top: 80, right: 460 }]} />
      </View>
    )
  }
}
